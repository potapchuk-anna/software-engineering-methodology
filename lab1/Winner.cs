public class Winner {
    public int winnerNumber;
    public (int,int)? winningCell;

    public Winner(int winnerNumber)
    {
        this.winnerNumber = winnerNumber;
    }

     public Winner(int winnerNumber, (int,int) winningCell)
    {
        this.winnerNumber = winnerNumber;
        this.winningCell = winningCell;
    }
    public override string ToString(){
         return winningCell == null 
        ? $"{winnerNumber}\n" 
        : $"{winnerNumber}\n{winningCell.Value.Item1} {winningCell.Value.Item2}\n";
    }  
}