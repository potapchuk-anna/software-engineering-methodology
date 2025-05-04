public class Matrix{

    const int MATRIX_ROW_AMOUNT = 19;
    const int MATRIX_COLUMN_AMOUNT = 19;

    public static List<int[,]> ReadMatricesFromFile(string filePath)
    {
        List<int[,]> matrices = new List<int[,]>();
        string[] lines = File.ReadAllLines(filePath);
        int matrixCount = int.Parse(lines[0]); // Read the number of matrices

        int index = 1; // Start reading matrices after the first line
        for (int m = 0; m < matrixCount; m++)
        {
            int[,] matrix = new int[MATRIX_ROW_AMOUNT, MATRIX_COLUMN_AMOUNT];
            for (int i = 0; i < MATRIX_ROW_AMOUNT; i++)
            {
                int[] row = lines[index].Split(' ').Select(int.Parse).ToArray();
                for (int j = 0; j < MATRIX_COLUMN_AMOUNT; j++)
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