using System.Data;
using Polly;

namespace dj_service;

public interface IDbAccess
{
    IDbConnection GetConnection();
    ISyncPolicy GetAccessPolicy();
}
