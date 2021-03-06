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
using System.IO;

using FluorineFx.Util;
using FluorineFx.Messaging.Rtmp.Event;

namespace FluorineFx.Messaging.Rtmp
{
	/// <summary>
	/// This type supports the Fluorine infrastructure and is not intended to be used directly from your code.
	/// </summary>
    [CLSCompliant(false)]
    public sealed class RtmpPacket
	{
		RtmpHeader		_header;
		IRtmpEvent		_message;
		ByteBuffer		_data;

        internal RtmpPacket(RtmpHeader header)
		{
			_header = header;
			_data = ByteBuffer.Allocate(header.Size + (header.Timer == 0xffffff ? 4 : 0));
		}

        internal RtmpPacket(RtmpHeader header, IRtmpEvent message)
		{
			_header = header;
			_message = message;
		}
        /// <summary>
        /// Gets or sets the RTMP header.
        /// </summary>
		public RtmpHeader Header
		{ 
			get{ return _header; }
			set{ _header = value; } 
		}
        /// <summary>
        /// Gets or sets the RTMP event.
        /// </summary>
		public IRtmpEvent Message
		{ 
			get{ return _message; }
			set{ _message = value; } 
		}
        /// <summary>
        /// Gets or sets packet data.
        /// </summary>
		public ByteBuffer Data
		{ 
			get{ return _data; }
			set{ _data = value; } 
		}
	}
}
