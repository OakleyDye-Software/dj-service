
using Dapper;

namespace dj_service;

public class SubmissionAccess(IDbAccess dbAccess) : ISubmissionAccess
{
    private readonly IDbAccess _dbAccess = dbAccess;
    public async Task<int> CreateContactFormSubmissionAsync(ContactFormSubmission submission) => 
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().QuerySingleAsync<int>(
                sql: @"
                    INSERT INTO app.contact_form_submission
                    (
                        first_name,
                        last_name,
                        email,
                        phone_number,
                        event_type_id,
                        event_date,
                        venue_location,
                        message
                    )
                    VALUES
                    (
                        @FirstName,
                        @LastName,
                        @Email,
                        @PhoneNumber,
                        @EventTypeId,
                        @DateOfEvent,
                        @VenueLocation,
                        @EventDescription
                    )
                    RETURNING id;",
                param: submission
            ));
}
