
using System;

namespace Service.Interface
{
    public interface IPasswordService
    {

        Tuple<byte[], byte[]> CreatePasswordHash(string password);
        bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
    }
}
