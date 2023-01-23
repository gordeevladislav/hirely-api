namespace Hirely.Common.Exceptions
{
  public class HirelyValidationException : Exception
  {
    public HirelyValidationException()
    {
    }

    public HirelyValidationException(Exception innerException) : base("Validation error occured", innerException)
    {
    }

    public HirelyValidationException(Dictionary<string, ICollection<string>> errors) : base("Validation error occured")
    {
      foreach (var e in errors)
      {
        foreach (var errMsg in e.Value)
        {
          _errors.Add(new KeyValuePair<string, string>(e.Key, errMsg));
        }
      }
    }

    public HirelyValidationException(string feildName, string errMsg) : base("Validation error occured")
    {
      _errors.Add(new KeyValuePair<string, string>(feildName, errMsg));
    }

    public IReadOnlyCollection<KeyValuePair<string, string>> Errors
    {
      get
      {
        return (IReadOnlyCollection<KeyValuePair<string, string>>)_errors;
      }
    }

    private readonly ICollection<KeyValuePair<string, string>> _errors = new List<KeyValuePair<string, string>>();
  }
}