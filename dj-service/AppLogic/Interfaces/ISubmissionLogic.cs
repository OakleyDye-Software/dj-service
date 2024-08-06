namespace dj_service;

public interface ISubmissionLogic
{
    Task<bool> CreateContactFormSubmissionAsync(ContactFormSubmission submission, ClientInfo client);
}
