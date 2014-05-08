//-----------------------------------------------------------------------------
//
// <copyright file="ILockBytesOverStream.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:  ILockBytes implementation over a Stream
//
//-----------------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;


namespace Microsoft.InformationProtectionAndControl
{

    internal class ILockBytesOverStream : ILockBytes
    {
        private Stream stream;

        public ILockBytesOverStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (!stream.CanSeek)
            {
                throw new ArgumentException("The passed in stream must be seekable", "stream");
            }
            this.stream = stream;
        }

        public void ReadAt(ulong offset, byte[] buffer, int count, out int bytesRead)
        {
            if (buffer.Length < count)
            {
                throw new ArgumentException("Requesting more bytes from the stream than will fit in the supplied buffer", "count");
            }

            int bytesToRead = count;
            bytesRead = 0;

            this.stream.Seek((long)offset, SeekOrigin.Begin);

            // Read may return fewer bytes than requested even if there are more bytes available.  We
            // keep reading from the stream until we've gathered the request number, or hit the EOF.
            while (bytesToRead > 0)
            {
                int currentRead = this.stream.Read(buffer, bytesRead, bytesToRead);

                if (currentRead == 0)
                {
                    break;
                }

                bytesToRead -= currentRead;
                bytesRead += currentRead;
            }
        }

        public void WriteAt(ulong offset, byte[] buffer, int count, out int written)
        {
            this.stream.Seek((long)offset, SeekOrigin.Begin);
            this.stream.Write(buffer, 0, count);
            written = count;
        }

        public void Flush()
        {
            this.stream.Flush();
        }

        public void SetSize(ulong length)
        {
            this.stream.SetLength((long)length);
        }
        
        public void LockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
        }

        public void UnlockRegion(ulong libOffset, ulong cb, int dwLockType)
        {
        }

        public void Stat(out ComTypes.STATSTG pstatstg, STATFLAG grfStatFlag)
        {
            pstatstg = new ComTypes.STATSTG();
            pstatstg.type = (int)STGTY.Stream;
            pstatstg.cbSize = this.stream.Length;
            pstatstg.grfLocksSupported = (int)LOCKTYPE.Exclusive;
        }
    }
}
