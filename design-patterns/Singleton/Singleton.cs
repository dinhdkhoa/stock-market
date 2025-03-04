namespace design_patterns;

public class Singleton
{
    private static Singleton? instance;

    protected Singleton()
    {
        
    }
    public string Settings { get; private set; } = "Color Red";

    public static Singleton Instance()
    {
        return instance ??= new Singleton();
    }
}


// var object1 = Singleton.Instance();
// var object2 = Singleton.Instance();
//
// if(object1 == object2) always true