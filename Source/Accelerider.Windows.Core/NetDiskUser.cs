﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Accelerider.Windows.Infrastructure;
using Accelerider.Windows.Infrastructure.Interfaces;

namespace Accelerider.Windows.Core
{
    internal class NetDiskUser : INetDiskUser
    {

        public Uri HeadImageUri => throw new NotImplementedException();

        public string Username => "Laplace's Domon";

        public string Nickname => "LD50";

        public DataSize TotalCapacity => new DataSize(5, SizeUnitEnum.T);

        public DataSize UsedCapacity => new DataSize(2.34, SizeUnitEnum.T);

        public Task<bool> DeleteFileAsync(INetDiskFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<ITreeNodeAsync<INetDiskFile>> GetNetDiskFileTreeAsync()
        {
            var rand = new Random();
            await Task.Delay(1000);
            var tree = new TreeNodeAsync<INetDiskFile>(new NetDiskFile { FilePath = new FileLocation("E:\\") })
            {
                ChildrenProvider = async parent =>
                {
                    await Task.Delay(1000);
                    var filePaths = Directory.GetFiles(parent.FilePath.ToString());
                    var directoriePaths = Directory.GetDirectories(parent.FilePath.ToString());
                    return from filePath in directoriePaths.Union(filePaths)
                           where File.Exists(filePath) || Directory.Exists(filePath)
                           select new NetDiskFile
                           {
                               FilePath = new FileLocation(filePath),
                               FileSize = File.Exists(filePath) ? new DataSize(new FileInfo(filePath).Length) : default(DataSize),
                               ModifiedTime = new FileInfo(filePath).LastWriteTime
                           };
                }
            };
            return tree;
        }

        public async Task<IEnumerable<IDeletedFile>> GetDeletedFilesAsync()
        {
            await Task.Delay(1000);
            var rand = new Random();
            const string folderPath = @"E:\ionic\MyIonicProject";
            var filePaths = Directory.GetFiles(folderPath);
            var directoriePaths = Directory.GetDirectories(folderPath);
            return from filePath in directoriePaths.Union(filePaths)
                   where File.Exists(filePath) || Directory.Exists(filePath)
                   select new DeletedFile
                   {
                       FilePath = new FileLocation(filePath),
                       LeftDays = rand.Next(1, 11),
                       FileSize = File.Exists(filePath) ? new DataSize(new FileInfo(filePath).Length) : default(DataSize),
                       DeletedTime = new FileInfo(filePath).LastWriteTime
                   };
        }

        public Task<IEnumerable<ISharedFile>> GetSharedFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(ShareStateCode, ISharedFile)> ShareFilesAsync(IEnumerable<INetDiskFile> files, string password = null)
        {
            throw new NotImplementedException();
        }

        public ITransferTaskToken CreateUploadTask(FileLocation filePath)
        {
            throw new NotImplementedException();
        }

        public ITransferTaskToken CreateDownloadTask(INetDiskFile file)
        {
            throw new NotImplementedException();
        }
    }
}