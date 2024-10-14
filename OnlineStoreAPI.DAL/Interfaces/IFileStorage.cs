using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IFileStorage
    {
        /// <summary>
        /// Add object to filestorage
        /// </summary>
        /// <param name="file">Your file in bytes</param>
        /// <param name="fileName">Your file name with extesion</param>
        /// <param name="customPath">You can write your custom path. For example: "photo/name/..."</param>
        /// <returns>After operation you get "key" of your object</returns>
        public Task<string> AddAsync(byte[] file, string fileName, string customPath = null);

        /// <summary>
        /// Add object to filestorage
        /// </summary>
        /// <param name="fileBase64">Your file in base64 type</param>
        /// <param name="fileName">Your file name with extesion</param>
        /// <param name="customPath">You can write your custom path. For example: "photo/name/..."</param>
        /// <returns>After operation you get "key" of your object</returns>
        public Task<string> AddAsync(string fileBase64, string fileName, string customPath = null);

        /// <summary>
        /// Get object "URL" from filestorage
        /// </summary>
        /// <param name="key">Key of object</param>
        /// <param name="expiryTimeInMinutes">Time for URL. In minutes</param>
        /// <returns>URL</returns>
        public Task<string> GetUrlAsync(string key, int expiryTimeInMinutes = 24 * 60);

        /// <summary>
        /// Delete object by "Key"
        /// </summary>
        /// <param name="key">Key of object</param>
        /// <returns></returns>
        public Task DeleteAsync(string key);
    }
}
