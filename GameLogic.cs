namespace 3task
{
    static class GameRules
    {
        public static int GetWinner(int indexSide1, int indexSide2, int arrayLength)
        {
            int diff = indexSide1 - indexSide2;

            int halfLength = arrayLength / 2;

            if (diff == 0)
            {
                return -1;
            }
            else if (diff < 0)
            {
                if (-diff <= halfLength) return indexSide2;
                else return indexSide1;
            }
            else
            {
                if (diff <= halfLength) return indexSide1;
                else return indexSide2;
            }
        }
    }
}
