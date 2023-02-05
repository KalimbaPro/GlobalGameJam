using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
        public bool boss = false;
    }
    public class Neighbor
    {
        public int type = WALL;
        public int cell = -1;
    }

    public Vector2Int size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;

    public bool[] firstRoom;
    List<Cell> midBoard;
    List<Cell> topBoard;
    List<Cell> botBoard;
    List<Cell> rightBoard;

    private void Awake()
    {
        startPos = (size.y / 2) * size.x;
        gridGen(ref midBoard, startPos);
        int topConnector = Random.Range(0, size.x - 1);
        while (midBoard[topConnector].generated == false)
           topConnector = Random.Range(0, size.x - 1);

        int botConnector = Random.Range(0, size.x - 1);
        while (midBoard[(size.y - 1) * size.x + botConnector].generated == false)
            botConnector = Random.Range(0, size.x - 1);

        int rightConnector = Random.Range(0, size.y - 1);
        while (midBoard[rightConnector * size.y + size.x - 1].generated == false)
            rightConnector = Random.Range(0, size.y - 1);

        gridGen(ref topBoard, (size.y - 1) * size.x + topConnector);
        gridGen(ref botBoard, botConnector);
        gridGen(ref rightBoard, rightConnector * size.y);

        midBoard[topConnector].status[TOP] = true;
        midBoard[(size.y - 1) * size.x + botConnector].status[BOT] = true;
        midBoard[rightConnector * size.y + size.x - 1].status[RIGHT] = true;

        // mid
        var distances = Dijkstra(midBoard, startPos);
        int farthestRoom = getFarthestRoom(distances);
        midBoard[farthestRoom].boss = true;
        midBoard[farthestRoom].status = new bool[] {false, false, false, false};

        // top
        distances = Dijkstra(topBoard, (size.y - 1) * size.x + topConnector);
        farthestRoom = getFarthestRoom(distances);
        topBoard[farthestRoom].boss = true;
        topBoard[farthestRoom].status = new bool[] { false, false, false, false };

        // bot
        distances = Dijkstra(botBoard, botConnector);
        farthestRoom = getFarthestRoom(distances);
        botBoard[farthestRoom].boss = true;
        botBoard[farthestRoom].status = new bool[] { false, false, false, false };

        // right
        distances = Dijkstra(rightBoard, rightConnector * size.y);
        farthestRoom = getFarthestRoom(distances);
        rightBoard[farthestRoom].boss = true;
        rightBoard[farthestRoom].status = new bool[] { false, false, false, false };

        dungeonGen(midBoard, new Vector2(0, 0));
        dungeonGen(topBoard, new Vector2(0, size.y * offset.y));
        dungeonGen(botBoard, new Vector2(0, -size.y * offset.y));
        dungeonGen(rightBoard, new Vector2(size.x * offset.x, 0));
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    int getFarthestRoom(List<int> distances)
    {
        int max = distances.Max();
        List<int> farthest = new List<int>();
        for (int i = 0; i < distances.Count; ++i)
            if (distances[i] == max)
                farthest.Add(i);
        return farthest[Random.Range(0, farthest.Count - 1)];
    }

    private class Node
    {
        public int position;
        public int distance;
        public Node(int position, int distance)
        {
            this.position = position;
            this.distance = distance;
        }
    }

    List<int> Dijkstra(List<Cell> board, int startPos)
    {
        List<int> dist_list = new List<int>(size.x * size.y);
        List<bool> visited = new List<bool>(size.x * size.y);
        for (int i = 0; i < (size.x * size.y); i++)
        {
            dist_list.Add(int.MaxValue);
            visited.Add(false);
        }
        dist_list[startPos] = 0;
        Queue<Node> queue = new Queue<Node>();
        queue.Enqueue(new Node(startPos, 0));
        while (queue.Count > 0)
        {
            Node node = queue.Dequeue();
            int position = node.position;
            int x = position % size.y;
            int y = position / size.y;
            int distance = node.distance;

            if (visited[position])
                continue;

            visited[position] = true;
            
            // top
            if (board[position].status[TOP])
                UpdateDistance(queue, x, y - 1, distance + 1, dist_list);

            // bot
            if (board[position].status[BOT])
                UpdateDistance(queue, x, y + 1, distance + 1, dist_list);

            // left
            if (board[position].status[LEFT])
                UpdateDistance(queue, x - 1, y, distance + 1, dist_list);

            // right
            if (board[position].status[RIGHT])
                UpdateDistance(queue, x + 1, y, distance + 1, dist_list);
        }

        for (int i = 0; i < dist_list.Count; ++i)
        {
            if (dist_list[i] == int.MaxValue)
                dist_list[i] = -1;
        }
        return dist_list;
    }

    void UpdateDistance(Queue<Node> queue, int x, int y, int distance, List<int> dist_list)
    {
        if (x >= 0 && x < size.x && y >= 0 && y < size.y)
        {
            if (distance < dist_list[x + y * size.x])
            {
                dist_list[x + y * size.x] = distance;
                queue.Enqueue(new Node(x + y * size.x, distance));
            }
        }
    }

    void dungeonGen(List<Cell> board, Vector2 pos)
    {

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (board[Mathf.FloorToInt(i + j * size.x)].generated)
                {
                    var newRoom = Instantiate(room, new Vector3((i - (startPos % size.x)) * offset.x + pos.x, -(j - Mathf.FloorToInt(startPos / size.x)) * offset.y + pos.y, 0), Quaternion.identity, transform).GetComponent<RoomBehavior>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i + j * size.x)].status, board[Mathf.FloorToInt(i + j * size.x)].boss);
                }
            }
        }
    }

    void gridGen(ref List<Cell> board, int startPos)
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


            List<Neighbor> neighbors = getPossibleDir(currentCell, board);
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
    }

    bool isEnd(List<Neighbor> neighbors)
    {
        for (int i = 0; i < neighbors.Count; ++i)
            if (neighbors[i].type == 0)
                return false;
        return true;
    }


    List<Neighbor> getPossibleDir(int cell, List<Cell> board)
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
        // print("add T: " + dirs[0].type);

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
        // print("add R: " + dirs[0].type + " " + dirs[1].type);

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
        // print("add B: " + dirs[0].type + " " + dirs[1].type + " " + dirs[2].type);

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
        // print("add L: " + dirs[0].type + " " + dirs[1].type + " " + dirs[2].type + " " + dirs[3].type);

        return dirs;
    }
}
