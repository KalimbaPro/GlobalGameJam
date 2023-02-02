using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGenerator : MonoBehaviour
{
    public static List<Transform> WomenSpawn = new List<Transform>();
    public Transform[] Women;

    private const int TOP = 0;
    private const int RIGHT = 1;
    private const int BOT = 2;
    private const int LEFT = 3;

    private const int WALL = -1;
    private const int EMPTY = 0;
    private const int DOOR = 1;
    public class Cell
    {
        public bool generated = false;
        public bool[] status = new bool[4];
    }
    public class Neighbor
    {
        public int type = WALL;
        public int cell = -1;
    }

    public Vector2 size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;

    public bool[] firstRoom;
    List<Cell> board;
    private void Awake()
    {
        gridGen();
    }
    // Start is called before the first frame update
    void Start()
    {
        //spawnWomen();
    }

    void spawnWomen()
    {
        foreach (var woman in Women)
        {
            int index = Random.Range(0, WomenSpawn.Count);
            var pos = WomenSpawn[index];
            woman.position = pos.position;
            WomenSpawn.RemoveAt(index);
        }
    }

    void dungeonGen()
    {

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (board[Mathf.FloorToInt(i + j * size.x)].generated)
                {
                    var newRoom = Instantiate(room, new Vector3((i - (startPos % size.x)) * offset.x, -(j - Mathf.FloorToInt(startPos / size.x)) * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status);
                }
            }
        }
    }

    void gridGen()
    {
        board = new List<Cell>();
        for (int i = 0; i < (size.x * size.y); i++)
        {
            board.Add(new Cell());
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;
        while (true)
        {
            k++;


            List<Neighbor> neighbors = getPossibleDir(currentCell);
            if (board[currentCell].generated)
            {
                if (isEnd(neighbors))
                {
                    if (path.Count == 0)
                    {
                        break;
                    }
                    else
                        currentCell = path.Pop();
                }
                else
                {
                    // choose a dir
                    int dir = 0;
                    do
                    {
                        dir = Random.Range(0, 4);
                    }
                    while (neighbors[dir].type != 0);
                    path.Push(currentCell);
                    currentCell = neighbors[dir].cell;
                }
            }
            else
            {
                // generate
                if (currentCell == startPos)
                {
                    board[currentCell].status = firstRoom;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        switch (neighbors[i].type)
                        {
                            case WALL:
                                board[currentCell].status[i] = false;
                                break;
                            case DOOR:
                                board[currentCell].status[i] = true;
                                break;
                            default:
                                //board[currentCell].status[i] = true;
                                board[currentCell].status[i] = (Random.Range(0, 4) != 0);
                                break;
                        }
                    }
                }
                board[currentCell].generated = true;
            }
        }

        dungeonGen();
    }

    bool isEnd(List<Neighbor> neighbors)
    {
        for (int i = 0; i < neighbors.Count; ++i)
            if (neighbors[i].type == 0)
                return false;
        return true;
    }


    List<Neighbor> getPossibleDir(int cell)
    {
        List<Neighbor> dirs = new List<Neighbor>();
        Cell c = board[cell];

        //check TOP
        Neighbor T = new Neighbor();
        if (cell - size.x >= 0)
        {
            T.cell = Mathf.FloorToInt(cell - size.x);
            if (c.generated)
            {
                if (c.status[TOP])
                {
                    if (board[T.cell].generated)
                        T.type = DOOR;
                    else
                        T.type = EMPTY;
                }
                else
                {
                    T.type = WALL;
                }
            }
            else
            {
                //print("a");
                if (board[T.cell].generated)
                {
                    //print("b");
                    if (board[T.cell].status[BOT])
                    {
                        //print("c");
                        T.type = DOOR;
                    }
                    else
                    {
                        //print("d");
                        T.type = WALL;
                    }
                }
                else
                {
                    //print("e");
                    T.type = EMPTY;
                }
            }
        }
        else
        {
            T.type = WALL;
        }
        dirs.Add(T);
        //print("add T: " + dirs[0].type);

        //check RIGHT
        Neighbor R = new Neighbor();
        if ((cell + 1) % size.x != 0)
        {
            R.cell = cell + 1;
            if (c.generated)
            {
                if (c.status[RIGHT])
                    if (board[R.cell].generated)
                        R.type = DOOR;
                    else
                        R.type = EMPTY;
                else
                {
                    R.type = WALL;
                }
            }
            else
            {
                if (board[R.cell].generated)
                {
                    if (board[R.cell].status[LEFT])
                        R.type = DOOR;
                    else
                    {
                        R.type = WALL;
                    }
                }
                else
                    R.type = EMPTY;
            }
        }
        else
        {
            R.type = WALL;
        }
        dirs.Add(R);
        //print("add R: " + dirs[0].type + " " + dirs[1].type);

        //check BOT
        Neighbor B = new Neighbor();
        if (cell + size.x < board.Count)
        {
            B.cell = Mathf.FloorToInt(cell + size.x);
            if (c.generated)
            {
                if (c.status[BOT])
                {
                    if (board[B.cell].generated)
                        B.type = DOOR;
                    else
                        B.type = EMPTY;
                }
                else
                {
                    B.type = WALL;
                }
            }
            else
            {
                if (board[B.cell].generated)
                {
                    if (board[B.cell].status[TOP])
                        if (board[B.cell].generated)
                            B.type = DOOR;
                        else
                            B.type = EMPTY;
                    else
                    {
                        B.type = WALL;
                    }
                }
                else
                    B.type = EMPTY;
            }
        }
        else
        {
            B.type = WALL;
        }
        dirs.Add(B);
        //print("add B: " + dirs[0].type + " " + dirs[1].type + " " + dirs[2].type);

        //check LEFT
        Neighbor L = new Neighbor();
        if (cell % size.x != 0)
        {
            L.cell = cell - 1;
            if (c.generated)
            {
                if (c.status[LEFT])
                    if (board[L.cell].generated)
                        L.type = DOOR;
                    else
                        L.type = EMPTY;
                else
                {
                    L.type = WALL;
                }
            }
            else
            {
                if (board[L.cell].generated)
                {
                    if (board[L.cell].status[RIGHT])
                        L.type = DOOR;
                    else
                    {
                        L.type = WALL;
                    }
                }
                else
                    L.type = EMPTY;
            }
        }
        else
        {
            L.type = WALL;
        }
        dirs.Add(L);
        //print("add L: " + dirs[0].type + " " + dirs[1].type + " " + dirs[2].type + " " + dirs[3].type);

        return dirs;
    }
}