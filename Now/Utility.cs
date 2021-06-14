using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Now {
	public static class Utility {

		public static HttpWebResponse PostData(string requestURI , string payLoad)
		{
			HttpWebResponse response;            
            var request = (HttpWebRequest)WebRequest.Create(requestURI);
            request.Host = "app.sharesies.com";            
            request.Headers["sec-ch-ua"] = " \"Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\";";
            request.Headers["x-version"] = "9593";
            request.Headers["DNT"] = "1";
            request.Headers["x-api-version"] = "33";
            request.Headers["sec-ch-ua-mobile"] = "?0";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
            request.Headers["x-known-device-key"] = "fded2945-3566-4c96-9e66-1264373ea09b";
            request.Headers["x-git-hash"] = "7ab4eb9847185e7b5518da901ed792b01de314ff";
            request.Accept = "*/*";
            request.Headers["Origin"] = "https://app.sharesies.com";
            request.Headers["Sec-Fetch-Site"] = "same-origin";
            request.Headers["Sec-Fetch-Mode"] = "cors";
            request.Headers["Sec-Fetch-Dest"] = "empty";
            request.Headers["Referer"] = "https://app.sharesies.com/login?_ga=2.153184378.310525915.1621999829-1590703744.1621999829";
            request.Headers["Accept-Encoding"] = "gzip, deflate, br";
            request.Headers["Accept-Language"] = "en-GB,en-US;q=0.9,en;q=0.8";
            var data = Encoding.ASCII.GetBytes(payLoad);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream()) {
                stream.Write(data, 0, data.Length);
            }
            response = (HttpWebResponse)request.GetResponse();
            return response;
		}


        public static HttpWebResponse GetData(string requestURI, string payLoad, NameValueCollection headers) {
            HttpWebResponse response;
            
            var request = (HttpWebRequest)WebRequest.Create(requestURI);
            foreach (var nv in headers) {
                request.Headers.Add(nv.ToString(), headers[nv.ToString()]);
            }
            
            request.Host = "app.sharesies.com";
            request.Headers["sec-ch-ua"] = " \"Not; A Brand\";v=\"99\", \"Google Chrome\";v=\"91\", \"Chromium\";v=\"91\";";
            request.Headers["x-version"] = "9593";
            request.Headers["DNT"] = "1";
            request.Headers["x-api-version"] = "33";
            request.Headers["sec-ch-ua-mobile"] = "?0";
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
            request.Headers["x-known-device-key"] = "fded2945-3566-4c96-9e66-1264373ea09b";
            request.Headers["x-git-hash"] = "7ab4eb9847185e7b5518da901ed792b01de314ff";
            request.Accept = "*/*";
            request.Headers["Origin"] = "https://app.sharesies.com";
            request.Headers["Sec-Fetch-Site"] = "same-origin";
            request.Headers["Sec-Fetch-Mode"] = "cors";
            request.Headers["Sec-Fetch-Dest"] = "empty";
            request.Headers["Referer"] = "https://app.sharesies.com/login?_ga=2.153184378.310525915.1621999829-1590703744.1621999829";
            request.Headers["Accept-Encoding"] = "gzip, deflate, br";
            request.Headers["Accept-Language"] = "en-GB,en-US;q=0.9,en;q=0.8";
            var data = Encoding.ASCII.GetBytes(payLoad);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            //request.ContentLength = data.Length;
            //using (var stream = request.GetRequestStream()) {
            //    stream.Write(data, 0, data.Length);
            //}
            response = (HttpWebResponse)request.GetResponse();
            return response;
        }


        public static byte[] StreamToByteArray(Stream input) {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static byte[] ExtractGZipData(byte[] data) {
            using (var compressedStream = new MemoryStream(data))
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            using (var resultStream = new MemoryStream()) {
                zipStream.CopyTo(resultStream);
                return resultStream.ToArray();
            }
        }
    }
}
