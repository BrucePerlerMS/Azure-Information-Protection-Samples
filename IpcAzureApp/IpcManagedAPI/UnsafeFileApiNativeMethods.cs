// Copyright © Microsoft Open Technologies, Inc.
//
// All Rights Reserved
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS
// OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION
// ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A
// PARTICULAR PURPOSE, MERCHANTABILITY OR NON-INFRINGEMENT.
//
// See the Apache License, Version 2.0 for the specific language
// governing permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.InformationProtectionAndControl
{
    internal static class UnsafeFileApiMethods
    {
        private const string fileAPIDLLName = "msipc.dll";
        public static string FileAPIDLLName
        {
            get { return fileAPIDLLName; }
        }

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfEncryptFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [In] IntPtr pvLicenseInfo,
            [In, MarshalAs(UnmanagedType.U4)] uint dwType,
            [In, MarshalAs(UnmanagedType.U4)] uint dwFlags,
            [In, MarshalAs(UnmanagedType.LPStruct)] IpcPromptContext pContext,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszOutputFileDirectory,
            [Out] out IntPtr wszOutputFilePath);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfEncryptFileStream(
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes pInputFileStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [In] IntPtr pvLicenseInfo,
            [In, MarshalAs(UnmanagedType.U4)] uint dwType,
            [In, MarshalAs(UnmanagedType.U4)] uint dwFlags,
            [In, MarshalAs(UnmanagedType.LPStruct)] IpcPromptContext pContext,
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes pOutputFileStream,
            [Out] out IntPtr wszOutputFilePath);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfDecryptFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [In, MarshalAs(UnmanagedType.U4)] uint dwFlags,
            [In, MarshalAs(UnmanagedType.LPStruct)] IpcPromptContext pContext,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszOutputFileDirectory,
            [Out] out IntPtr wszOutputFilePath);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfDecryptFileStream(
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes pInputFileStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [In, MarshalAs(UnmanagedType.U4)] uint dwFlags,
            [In, MarshalAs(UnmanagedType.LPStruct)] IpcPromptContext pContext,
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes pOutputFileStream,
            [Out] out IntPtr wszOutputFilePath);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfGetSerializedLicenseFromFile(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [Out] out IntPtr pvLicense);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfGetSerializedLicenseFromFileStream(
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes inputFileStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [Out] out IntPtr pvLicense);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfIsFileEncrypted(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [Out] out uint dwFileStatus);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcfIsFileStreamEncrypted(
            [In, MarshalAs(UnmanagedType.Interface)] ILockBytes inputFileStream,
            [In, MarshalAs(UnmanagedType.LPWStr)] string wszInputFilePath,
            [Out] out uint dwFileStatus);

        [DllImport(fileAPIDLLName, SetLastError = false, CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern int IpcFreeMemory(
            [In] IntPtr handle);
    }
}
