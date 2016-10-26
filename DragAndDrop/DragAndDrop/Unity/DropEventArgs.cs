namespace DragAndDrop.Unity
{
    public class DropEventArgs
    {
        public DropEventArgs(object data, object target, DropType dropType)
        {
            Data = data;
            Target = target;
            DropType = dropType;
        }

        public object Data { get; set; }
        public object Target { get; set; }
        public DropType DropType { get; set; }
    }
}