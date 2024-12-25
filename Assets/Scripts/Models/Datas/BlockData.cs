namespace Models.Datas
{
    public class BlockData
    {
        public string Id;
        public float HorizontalPosition;

        public BlockData()
        {
        }

        public BlockData(string id, float newBlockPosition)
        {
            Id = id;
            HorizontalPosition = newBlockPosition;
        }
    }
}