using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace VerifyFIPSMode {
	class Program {
		static void Main(string[] args) {
			const string test = "The quick brown fox jumped over the lazy dog.";

			//Write out the standard message. Exceptions mean you're in FIPS mode.
			Console.WriteLine("VerifyFIPSMode Tool");
			Console.WriteLine("If any unhandled exceptions appear when you run this tool,");
			Console.WriteLine("this machine is running in FIPS mode.");

#region Microsoft Windows FIPS Compliant:

			HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");

#endregion

#region Not Microsoft Windows FIPS Compliant:

			HashAlgorithm sha256 = HashAlgorithm.Create("SHA256");
			HashAlgorithm sha384 = HashAlgorithm.Create("SHA384");
			HashAlgorithm sha512 = HashAlgorithm.Create("sha512");
			HashAlgorithm ripeMd160 = HashAlgorithm.Create("RIPEMD160");
			HashAlgorithm md5 = HashAlgorithm.Create("MD5");

#endregion
			
			//First, check the FIPS compliant cipher(s):
			Console.WriteLine("Testing SHA1:");
			Console.WriteLine(getHashOrErrorString(test, sha1));
			//Next, the non-FIPS compliant cipher(s)
			Console.WriteLine("Testing MD5:");
			Console.WriteLine(getHashOrErrorString(test, md5));
			Console.WriteLine("Testing RIPEMD160:");
			Console.WriteLine(getHashOrErrorString(test, ripeMd160));
			Console.WriteLine("Testing SHA256:");
			Console.WriteLine(getHashOrErrorString(test, sha256));
			Console.WriteLine("Testing SHA384:");
			Console.WriteLine(getHashOrErrorString(test, sha384));
			Console.WriteLine("Testing SHA512:");
			Console.WriteLine(getHashOrErrorString(test, sha512));
			Console.ReadLine();
		}
		//
		//Layout of this method found here:
		//http://stackoverflow.com/questions/4329909/hashing-passwords-with-md5-or-sha-256-c-sharp
		//
		/// <summary>
		/// Computes the hash of the string passed in.
		/// </summary>
		/// <param name="hashMe">The string to hash</param>
		/// <param name="hasher">The HashAlgorithm to use</param>
		/// <returns>The hash, or an error with stack trace</returns>
		static string getHashOrErrorString(string hashMe, HashAlgorithm hasher) {
			//The destination byte array:
			byte[] hashed;
			//Prepare the byte array for hashing:
			//This system for converting from string to byte[] was found here:
			//http://stackoverflow.com/questions/472906/net-string-to-byte-array-c-sharp
			//
			byte[] testBytes = new byte[hashMe.Length * sizeof(char)];
			System.Buffer.BlockCopy(hashMe.ToCharArray(), 0, testBytes, 0, testBytes.Length);
			//Try the hash
			try {
				hashed = hasher.ComputeHash(testBytes);
			} catch (Exception e) {
				//Return the error
				return e.Message + "\r\n---\r\n" + e.StackTrace;
			}
			//Return the result
			return BitConverter.ToString(hashed);
		}
	}
}
