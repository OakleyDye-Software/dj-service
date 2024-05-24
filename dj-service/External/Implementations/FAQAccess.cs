using Dapper;

namespace dj_service;

public class FAQAccess(IDbAccess dbAccess): IFAQAccess
{
    private readonly IDbAccess _dbAccess = dbAccess;
    public async Task<IEnumerable<FAQ>> GetFAQs() => 
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().QueryAsync<FAQ>(
                sql: @"
                    SELECT
                        id AS Id,
                        question AS Question,
                        answer AS Answer
                    FROM app.frequently_asked_question;"
            ));

    public async Task<FAQ> GetFAQ(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryFirstAsync<FAQ>(
                sql: @"
                    SELECT
                        id AS Id,
                        question AS Question,
                        answer AS Answer
                    FROM app.frequently_asked_question
                    WHERE id = @id;",
                param: new { id }
            ));

    public async Task<FAQ> AddFAQ(FAQ faq) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            var id = await _dbAccess.GetConnection().QuerySingleAsync<int>(
                sql: @"
                    INSERT INTO app.frequently_asked_question (question, answer)
                    VALUES (@Question, @Answer)
                    RETURNING id;",
                param: faq
            );
            return await GetFAQ(id);
        });

    public async Task<FAQ> UpdateFAQ(FAQ faq) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    UPDATE app.frequently_asked_question
                    SET question = @Question, answer = @Answer
                    WHERE id = @Id;",
                param: faq
            );
            return await GetFAQ(faq.Id);
        });

    public async Task<FAQ> DeleteFAQ(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
        {
            var faq = await GetFAQ(id);
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    DELETE FROM app.frequently_asked_question
                    WHERE id = @id;",
                param: new { id }
            );
            return faq;
        });
}
