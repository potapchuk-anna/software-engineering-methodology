class Program
{
    const int WINNING_COMBINATION_AMOUNT = 5;
     const int MATRIX_ROW_COLUMN_AMOUNT = 19;
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

        var checkFunctionsBool = new Func<int[,], bool, Winner>[]
        {
            CheckHorizontallyOrVertically,
            CheckHorizontallyOrVertically,
            CheckDiagonal,
            CheckDiagonal
        };

        bool isFirstDirection = true;
        foreach (var check in checkFunctionsBool)
        {
            var winner = check(matrix, isFirstDirection);
            if (winner.winnerNumber != 0)
            {
                return winner;
            }
            isFirstDirection = !isFirstDirection;
        }

        return new Winner(0);
    }

    static Winner CheckHorizontallyOrVertically(int[,] matrix, bool isHorizontally){
        int k=0;
        Winner? winner = null;

        for(int i = 0; i<MATRIX_ROW_COLUMN_AMOUNT;i++){

            for(int j=1;j<MATRIX_ROW_COLUMN_AMOUNT;j++){

                var rowIndex = isHorizontally ? i : j;
                var columnIndex = isHorizontally ? j : i;

                var previousCellIndexes = isHorizontally ? (rowIndex, columnIndex-1) : (rowIndex-1, columnIndex);

                if(matrix[rowIndex,columnIndex] == matrix[previousCellIndexes.Item1,previousCellIndexes.Item2] && (matrix[rowIndex,columnIndex] == 1 || matrix[rowIndex,columnIndex] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==WINNING_COMBINATION_AMOUNT-1){
                    var winningCellIndexes = isHorizontally ? (rowIndex, columnIndex-(WINNING_COMBINATION_AMOUNT - 1)) : (rowIndex-(WINNING_COMBINATION_AMOUNT - 1), columnIndex);
                   winner = new Winner(matrix[winningCellIndexes.Item1,winningCellIndexes.Item2], (winningCellIndexes.Item1+1, winningCellIndexes.Item2+1));
                }
                else if(k>WINNING_COMBINATION_AMOUNT-1){
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

    static Winner CheckDiagonal(int[,] matrix, bool isMainDiagonal)
    {
         int k=0;
        Winner? winner = null;

        int amountAndCombinationGap = MATRIX_ROW_COLUMN_AMOUNT - WINNING_COMBINATION_AMOUNT;

        int colStart = isMainDiagonal ? amountAndCombinationGap : 1;

        // Lower triangle
        for(int col=colStart; isMainDiagonal ? col>0 : col <= amountAndCombinationGap; col = isMainDiagonal ? col-1 : col+1){
            int j = isMainDiagonal ? 1 : MATRIX_ROW_COLUMN_AMOUNT-2;

            for(int i = col+1; i<MATRIX_ROW_COLUMN_AMOUNT; i++){
                var previousCellColumnIndex = isMainDiagonal ? j-1 : j+1;
                if(matrix[i,j] == matrix[i-1,previousCellColumnIndex] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==WINNING_COMBINATION_AMOUNT-1){
                    var winningColumnIndex = isMainDiagonal ? j-(WINNING_COMBINATION_AMOUNT - 1) : j+(WINNING_COMBINATION_AMOUNT - 1);
                    var winningRowIndex = i-(WINNING_COMBINATION_AMOUNT - 1);
                   winner = new Winner(matrix[winningRowIndex, winningColumnIndex], (winningRowIndex + 1, winningColumnIndex + 1));
                }
                else if(k>WINNING_COMBINATION_AMOUNT-1){
                    winner = null;
                }
                j = isMainDiagonal ? j+1 : j-1;
            }
            if(winner != null){
                return winner;
            }
            k=0;
        }

        // Upper triangle
        colStart = isMainDiagonal ? 0 : WINNING_COMBINATION_AMOUNT-1;
        for(int col=colStart; isMainDiagonal ? col<=amountAndCombinationGap : col < MATRIX_ROW_COLUMN_AMOUNT; col++){
            int i=1;
            for(int j = isMainDiagonal ? col+1 : col-1; isMainDiagonal ? j < MATRIX_ROW_COLUMN_AMOUNT : j >= 0; j = isMainDiagonal ? j+1 : j-1){
                var prevColumnIndex = isMainDiagonal? j-1:j+1;
                if(matrix[i,j] == matrix[i-1,prevColumnIndex] && (matrix[i,j] == 1 || matrix[i,j] == 2)){
                    k++;
                }
                else {
                    k=0;
                }
                if(k==WINNING_COMBINATION_AMOUNT-1){
                    var winningColumnIndex=isMainDiagonal ? j-(WINNING_COMBINATION_AMOUNT - 1) : j+(WINNING_COMBINATION_AMOUNT - 1);
                    var winningRowIndex = i-(WINNING_COMBINATION_AMOUNT - 1);
                   winner = new Winner(matrix[winningRowIndex, winningColumnIndex], (winningRowIndex + 1, winningColumnIndex + 1));
                }
                else if(k>WINNING_COMBINATION_AMOUNT-1){
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
}