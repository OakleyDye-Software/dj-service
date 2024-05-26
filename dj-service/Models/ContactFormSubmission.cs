namespace dj_service;

public class ContactFormSubmission
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public int EventTypeId { get; set; }
    public DateTime DateOfEvent { get; set; }
    public string VenueLocation { get; set; } = string.Empty;
    public string EventDescription { get; set; } = string.Empty;
    public string FullName { get => $"{FirstName} {LastName}"; }
}
