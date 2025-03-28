public class Matrix{

    public static List<int[,]> ReadMatricesFromFile(string filePath)
    {
        List<int[,]> matrices = new List<int[,]>();
        string[] lines = File.ReadAllLines(filePath);
        int matrixCount = int.Parse(lines[0]); // Read the number of matrices

        int index = 1; // Start reading matrices after the first line
        for (int m = 0; m < matrixCount; m++)
        {
            int[,] matrix = new int[19, 19];
            for (int i = 0; i < 19; i++)
            {
                int[] row = lines[index].Split(' ').Select(int.Parse).ToArray();
                for (int j = 0; j < 19; j++)
                {
                    matrix[i, j] = row[j];
                }
                index++;
            }
            matrices.Add(matrix);
        }

        return matrices;
    }

    public static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

}