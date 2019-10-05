namespace Domains.Article
{
  public class Author
  {
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string Institution { get; set; }
    public string PageUrl { get; set; }
    public string Email { get; set; }
    public string BibliographySummary { get; set; }
    public string FullName => $"{FirstName} {MiddleName} { LastName}";
  }
}