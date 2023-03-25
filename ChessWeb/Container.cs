using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChessWeb
{
    //public class NameMovePair
    //{
    //    public string Name { get; set; }
    //    public string Move { get; set; }
    //    public NameMovePair(string name, string move)
    //    {
    //        Name = name;
    //        Move = move;
    //    }
    //}
    public static class Container
    {
        //public static List<NameMovePair> MainContainer;
        public static Dictionary<string, string> MainContainer = new Dictionary<string, string>();

    }
}
