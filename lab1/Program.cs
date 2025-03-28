class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter file name:");
        var filePath = Console.ReadLine();
        if(string.IsNullOrEmpty(filePath) || !File.Exists(filePath)){
            Console.WriteLine("Such file does not exists");
            return;
        }
        
        ProcessFile(filePath);
    }

    static void ProcessFile(string filePath)
    {
        List<int[,]> matrices = Matrix.ReadMatricesFromFile(filePath);
        string result = "";

        foreach (var matrix in matrices)
        {
            var winner = FindWinner(matrix);
            result += winner?.ToString();
            Console.WriteLine(winner?.ToString());
        }

        File.WriteAllText("output.txt", result);
    }


    static Winner FindWinner(int[,] matrix){
        var checkFunctions = new Func<int[,], Winner>[]
        {
            CheckHorizontally,
            CheckVertically,
            CheckByMainDiagonal,
            CheckBySecondaryDiagonal
        };

        foreach (var check in checkFunctions)
        {
            var winner = check(matrix);
            if (winner.winnerNumber != 0)
            {
                return winner;
            }
        }

        return new Winner(0);
    }

    static Winner CheckHorizontally(int[,] matrix){
        int k=0;
        Winner? winner = null;
        for(int i = 0; i<matrix.GetLength(0);i++){

            for(int j=1;j<matrix.GetLength(1);j++){

                if(matrix[i,j] == matrix[i,j-1] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i,j-4], (i+1, j-3));
                }
                else if(k>4){
                    winner = null;
                }
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }
        return new Winner(0);
    }

    static Winner CheckVertically(int[,] matrix){
        int k=0;
        Winner? winner = null;
        for(int j = 0; j<matrix.GetLength(1);j++){

            for(int i=1;i<matrix.GetLength(0);i++){

                if(matrix[i,j] == matrix[i-1,j] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i-4,j], (i-3, j+1));
                }
                else if(k>4){
                    winner = null;
                }
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }
        return new Winner(0);
    }

    static Winner CheckByMainDiagonal(int[,] matrix){
        int k=0;
        Winner? winner = null;
        int n = matrix.GetLength(0);

         // Lower triangle
        for(int col=14; col>0; col--){
            int j=1;
            for(int i = col+1; i<n; i++){
                if(matrix[i,j] == matrix[i-1,j-1] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i-4,j-4], (i-3, j-3));
                }
                else if(k>4){
                    winner = null;
                }
                j++;
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }

        // Upper triangle (including main diagonal)
        for(int col=0; col<=14; col++){
            int i=1;
            for(int j = col+1; j<n; j++){
                if(matrix[i,j] == matrix[i-1,j-1] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i-4,j-4], (i-3, j-3));
                }
                else if(k>4){
                    winner = null;
                }
                i++;
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }

        return new Winner(0);
    }

    static Winner CheckBySecondaryDiagonal(int[,] matrix){
        int k=0;
        Winner? winner = null;
        int n = matrix.GetLength(0);

        // Upper triangle (including main diagonal)
        for(int col = 4; col < n; col++){
            int i = 1;
            for(int j = col-1; j >= 0; j--){
                if(matrix[i,j] == matrix[i-1,j+1] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i-4,j+4], (i-4+1, j+4+1));
                }
                else if(k>4){
                    winner = null;
                }
                i++;
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }

         // Lower triangle
        for(int col = 1; col <= 14; col++){
            int j = n-2;
            for(int i = col+1; i <n; i++){
                if(matrix[i,j] == matrix[i-1,j+1] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==4){
                   winner = new Winner(matrix[i-4,j+4], (i-4+1, j+4+1));
                }
                else if(k>4){
                    winner = null;
                }
                j--;
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }

        return new Winner(0);
    }
}