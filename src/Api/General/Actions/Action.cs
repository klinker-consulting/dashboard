namespace Dashboard.Api.General.Actions
{
    public class Action
    {
        public string Type { get; }

        public Action(string type)
        {
            Type = type;
        }
    }

    public class Action<TData> : Action
    {
        public TData Data { get; }

        public Action(string type, TData data)
            : base(type)
        {
            Data = data;
        }
    }
}
