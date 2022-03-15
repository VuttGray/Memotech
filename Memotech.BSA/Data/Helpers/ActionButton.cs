namespace Memotech.BSA.Data.Helpers
{
    public class ActionButton
    {
        public string Title { get; set; } = "";
        public string? ActionName { get; set; }
        public Action? Action { get; set; }
        public Func<bool>? VisibilityFunc { get; set; }
        public bool Visible => VisibilityFunc == null || VisibilityFunc();
        public bool Hidden => !Visible;
    }
}
