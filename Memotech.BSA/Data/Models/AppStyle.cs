namespace Memotech.BSA.Data.Models
{
    public class AppStyle
    {
        public string Color { get; set; } = "red";
        public string Size { get; set; } = "100px";
        public List<string> Words { get; set; } = new List<string>
        {
            "One", "Two", "Three"
        };
        public List<GridModel> GridModels { get; set; } = new List<GridModel>
        {
            new GridModel { Text = "Table Dark", Value = "table-dark"},
            new GridModel { Text = "Table Striped", Value = "table-striped"},
            new GridModel { Text = "Table Bordered", Value = "table-bordered"},
        };
    }
}
