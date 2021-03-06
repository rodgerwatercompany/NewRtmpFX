/*
	FluorineFx open source library 
	Copyright (C) 2007 Zoltan Csibi, zoltan@TheSilentGroup.com, FluorineFx.com 
	
	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.
	
	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
	Lesser General Public License for more details.
	
	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*/
using System;
using System.Collections;
using FluorineFx.AMF3;

namespace FluorineFx.IO.Writers
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
	class AMF3ObjectWriter : IAMFWriter
	{
		public AMF3ObjectWriter()
		{
		}
		#region IAMFWriter Members

		public bool IsPrimitive{ get{return false;} }

		public void WriteData(AMFWriter writer, object data)
		{
			if( data is IList )
			{
				//http://livedocs.macromedia.com/flex/2/docs/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=00001104.html#270405
				//http://livedocs.macromedia.com/flex/2/docs/wwhelp/wwhimpl/common/html/wwhelp.htm?context=LiveDocs_Parts&file=00001105.html#268711

				if( writer.UseLegacyCollection )
				{
					writer.WriteByte(AMF3TypeCode.Array);
					writer.WriteAMF3Array(data as IList);
				}
				else
				{
					writer.WriteByte(AMF3TypeCode.Object);
					object value = new ArrayCollection(data as IList);
					writer.WriteAMF3Object(value);
				}
				return;
			}
			if(data is IDictionary)
			{
				writer.WriteByte(AMF3TypeCode.Object);
				writer.WriteAMF3Object(data);
				return;
			}
			if(data is Exception)
			{
				writer.WriteByte(AMF3TypeCode.Object);
				writer.WriteAMF3Object(new ExceptionASO(data as Exception) );
				return;
			}
			if( data is IExternalizable )
			{
				writer.WriteByte(AMF3TypeCode.Object);
				writer.WriteAMF3Object(data);
				return;
			}

			writer.WriteByte(AMF3TypeCode.Object);
			writer.WriteAMF3Object(data);
		}

		#endregion
	}
}
