// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace nano.System.IO.Compression
{
	public enum CompressionMethod
	{
		Stored   = 0,
		Deflated = 8,
	}
	
	/// <summary>
	/// This class represents a member of a zip archive.  ZipFile and
	/// ZipInputStream will give you instances of this class as information
	/// about the members in an archive.  On the other hand ZipOutputStream
	/// needs an instance of this class to create a new member.
	///
	/// author of the original java version : Jochen Hoenicke
	/// </summary>
	public class ZipEntry : ICloneable
	{
		static int KNOWN_SIZE   = 1;
		static int KNOWN_CSIZE  = 2;
		static int KNOWN_CRC    = 4;
		static int KNOWN_TIME   = 8;
		
		string name;
		uint   size;
		ushort version;
		uint   compressedSize;
		uint   crc;
		uint   dosTime;
		
		ushort known = 0;
		CompressionMethod  method = CompressionMethod.Deflated;
		byte[] extra = null;
		string comment = null;
		bool   isCrypted;
		
		int zipFileIndex = -1;  /* used by ZipFile */
		int flags;              /* used by ZipOutputStream */
		int offset;             /* used by ZipFile and ZipOutputStream */
		
		public bool IsEncrypted {
			get {
				return (flags & 1) != 0; 
			}
			set {
				if (value) {
					flags |= 1;
				} else {
					flags &= ~1;
				}
			}
		}

		/// <summary>
		/// Get/Set index of this entry in Zip file
		/// </summary>
		/// <remarks>This is only valid when the entry is part of a <see cref="ZipFile"></see></remarks>
		public int ZipFileIndex {
			get {
				return zipFileIndex;
			}
			set {
				zipFileIndex = value;
			}
		}

		/// <summary>
		/// Get/set offset for use in central header
		/// </summary>
		public int Offset {
			get {
				return offset;
			}
			set {
				offset = value;
			}
		}

		/// <summary>
		/// Get/Set general purpose bit flag for entry
		/// </summary>
		public int Flags {                                // Stops having two things represent same concept in class (flag isCrypted removed)
			get { 
				return flags; 
			}
			set {
				flags = value; 
			}
		}
		
		
		/// <summary>
		/// Creates a zip entry with the given name.
		/// </summary>
		/// <param name="name">
		/// the name. May include directory components separated by '/'.
		/// </param>
		public ZipEntry(string name)
		{
			if (name == null)  {
				throw new ArgumentNullException("name");
			}
			this.DateTime  = DateTime.UtcNow;
			this.name = name;
		}
		
		/// <summary>
		/// Creates a copy of the given zip entry.
		/// </summary>
		/// <param name="e">
		/// the entry to copy.
		/// </param>
		public ZipEntry(ZipEntry e)
		{
			name           = e.name;
			known          = e.known;
			size           = e.size;
			compressedSize = e.compressedSize;
			crc            = e.crc;
			dosTime        = e.dosTime;
			method         = e.method;
			extra          = e.extra;
			comment        = e.comment;
		}

		/// <summary>
		/// Get minimum Zip feature version required to extract this entry
		/// </summary>
		public int Version {
			get {
				return version;
			}
			set {
				version = (ushort)value;
			}
		}

		/// <summary>
		/// Get/Set DosTime value.
		/// </summary>
		public long DosTime {
			get {
				if ((known & KNOWN_TIME) == 0) {
					return 0;
				} else {
					return dosTime;
				}
			}
			set {
				this.dosTime = (uint)value;
				known |= (ushort)KNOWN_TIME;
			}
		}
		
		
		/// <summary>
		/// Gets/Sets the time of last modification of the entry.
		/// </summary>
		public DateTime DateTime {
			get {
				uint sec  = 2 * (dosTime & 0x1f);
				uint min  = (dosTime >> 5) & 0x3f;
				uint hrs  = (dosTime >> 11) & 0x1f;
				uint day  = (dosTime >> 16) & 0x1f;
				uint mon  = ((dosTime >> 21) & 0xf);
				uint year = ((dosTime >> 25) & 0x7f) + 1980; /* since 1900 */
				return new DateTime((int)year, (int)mon, (int)day, (int)hrs, (int)min, (int)sec);
			}
			set {
				DosTime = ((uint)value.Year - 1980 & 0x7f) << 25 | 
				          ((uint)value.Month) << 21 |
				          ((uint)value.Day) << 16 |
				          ((uint)value.Hour) << 11 |
				          ((uint)value.Minute) << 5 |
				          ((uint)value.Second) >> 1;
			}
		}
		
		/// <summary>
		/// Returns the entry name.  The path components in the entry are
		/// always separated by slashes ('/').
		/// </summary>
		public string Name {
			get {
				return name;
			}
		}

		//		/// <summary>
		//		/// Gets/Sets the time of last modification of the entry.
		//		/// </summary>
		//		/// <returns>
		//		/// the time of last modification of the entry, or -1 if unknown.
		//		/// </returns>
		//		public long Time {
		//			get {
		//				return (known & KNOWN_TIME) != 0 ? time * 1000L : -1;
		//			}
		//			set {
		//				this.time = (int) (value / 1000L);
		//				this.known |= (ushort)KNOWN_TIME;
		//			}
		//		}

		/// <summary>
		/// Gets/Sets the size of the uncompressed data.
		/// </summary>
		/// <returns>
		/// The size or -1 if unknown.
		/// </returns>
		/// <remarks>Setting the size before adding an entry to an archive can help
		/// avoid compatibility problems with some archivers which don't understand Zip64 extensions.</remarks>
		public long Size {
			get {
				return (known & KNOWN_SIZE) != 0 ? (long)size : -1L;
			}
			set  {
				if (((ulong)value & 0xFFFFFFFF00000000L) != 0) {
					throw new ArgumentOutOfRangeException("size");
				}
				this.size  = (uint)value;
				this.known |= (ushort)KNOWN_SIZE;
			}
		}

		/// <summary>
		/// Gets/Sets the size of the compressed data.
		/// </summary>
		/// <returns>
		/// The compressed entry size or -1 if unknown.
		/// </returns>
		public long CompressedSize {
			get {
				return (known & KNOWN_CSIZE) != 0 ? (long)compressedSize : -1L;
			}
			set {
				if (((ulong)value & 0xffffffff00000000L) != 0) {
					throw new ArgumentOutOfRangeException();
				}
				this.compressedSize = (uint)value;
				this.known |= (ushort)KNOWN_CSIZE;
			}
		}
		
		/// <summary>
		/// Gets/Sets the crc of the uncompressed data.
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// if crc is not in 0..0xffffffffL
		/// </exception>
		/// <returns>
		/// the crc or -1 if unknown.
		/// </returns>
		public long Crc {
			get {
				return (known & KNOWN_CRC) != 0 ? crc & 0xffffffffL : -1L;
			}
			set {
				if (((ulong)crc & 0xffffffff00000000L) != 0) 
				{
					throw new Exception();
				}
				this.crc = (uint)value;
				this.known |= (ushort)KNOWN_CRC;
			}
		}
		
		/// <summary>
		/// Gets/Sets the compression method. Only DEFLATED and STORED are supported.
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// if method is not supported.
		/// </exception>
		/// <returns>
		/// the compression method or -1 if unknown.
		/// </returns>
		/// <see cref="ZipOutputStream.DEFLATED"/>
		/// <see cref="ZipOutputStream.STORED"/>
		public CompressionMethod CompressionMethod {
			get {
				return method;
			}
			set {
				this.method = value;
			}
		}
		
		/// <summary>
		/// Gets/Sets the extra data.
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// if extra is longer than 0xffff bytes.
		/// </exception>
		/// <returns>
		/// the extra data or null if not set.
		/// </returns>
		public byte[] ExtraData {
			get {
				return extra;
			}
			set {
				if (value == null) {
					this.extra = null;
					return;
				}
				
				if (value.Length > 0xffff) {
					throw new ArgumentOutOfRangeException();
				}
				this.extra = value;
				try {
					int pos = 0;
					while (pos < extra.Length) {
						int sig = (extra[pos++] & 0xff) | (extra[pos++] & 0xff) << 8;
						int len = (extra[pos++] & 0xff) | (extra[pos++] & 0xff) << 8;
						if (sig == 0x5455) {
							/* extended time stamp, unix format by Rainer Prem <Rainer@Prem.de> */
							int flags = extra[pos];
							if ((flags & 1) != 0) {
								int iTime = ((extra[pos+1] & 0xff) |
									(extra[pos+2] & 0xff) << 8 |
									(extra[pos+3] & 0xff) << 16 |
									(extra[pos+4] & 0xff) << 24);
								
								DateTime = new DateTime ( 1970, 1, 1, 0, 0, 0 ) + new TimeSpan ( 0, 0, 0, iTime, 0 );
								known |= (ushort)KNOWN_TIME;
							}
						}
						pos += len;
					}
				} catch (Exception) {
					/* be lenient */
					return;
				}
			}
		}
		
		/// <summary>
		/// Gets/Sets the entry comment.
		/// </summary>
		/// <exception cref="System.ArgumentOutOfRangeException">
		/// if comment is longer than 0xffff.
		/// </exception>
		/// <returns>
		/// the comment or null if not set.
		/// </returns>
		public string Comment {
			get {
				return comment;
			}
			set {
				if (value.Length > 0xffff) 
				{
					throw new ArgumentOutOfRangeException();
				}
				this.comment = value;
			}
		}
		
		/// <summary>
		/// Gets true, if the entry is a directory.  This is solely
		/// determined by the name, a trailing slash '/' marks a directory.
		/// </summary>
		public bool IsDirectory {
			get {
				int nlen = name.Length;
				return nlen > 0 && name[nlen - 1] == '/';
			}
		}
		
		/// <value>
		/// True, if the entry is encrypted.
		/// </value>
		public bool IsCrypted {
			get {
				return isCrypted;
			}
			set {
				isCrypted = value;
			}
		}
		
		/// <summary>
		/// Creates a copy of this zip entry.
		/// </summary>
		public object Clone()
		{
			return this.MemberwiseClone();
		}
		
		/// <summary>
		/// Gets the string representation of this ZipEntry.  This is just
		/// the name as returned by getName().
		/// </summary>
		public override string ToString()
		{
			return name;
		}
	}
}
