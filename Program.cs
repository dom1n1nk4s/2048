using _2048;

try
{
    GameManager.Start();
    while (true)
    {
        GameManager.Run();
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}