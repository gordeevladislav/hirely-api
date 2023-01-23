namespace Hirely.Common.Exceptions
{
  public class HirelyNotFoundException : Exception
  {
    public HirelyNotFoundException() : base("Object not found")
    {
    }

    public HirelyNotFoundException(string message) : base(message)
    {
    }
  }
}