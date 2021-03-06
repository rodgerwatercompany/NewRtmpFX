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
using FluorineFx.Configuration;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Endpoints;

namespace FluorineFx.Messaging
{
	/// <summary>
    /// The global scope that acts as root for all applications in a host.
	/// </summary>
	class GlobalScope : Scope, IGlobalScope
	{
        internal GlobalScope()
		{
		}


		#region IGlobalScope Members

        public FluorineFx.Messaging.Api.IServiceProvider ServiceProvider
        {
            get { return _serviceContainer; }
        }

		public void Register()
		{
            //Start services
            FluorineFx.Messaging.Rtmp.IO.IStreamableFileFactory streamableFileFactory = ObjectFactory.CreateInstance(FluorineConfiguration.Instance.FluorineSettings.StreamableFileFactory.Type) as FluorineFx.Messaging.Rtmp.IO.IStreamableFileFactory;
            AddService(typeof(FluorineFx.Messaging.Rtmp.IO.IStreamableFileFactory), streamableFileFactory, false);
            streamableFileFactory.Start(null);
            FluorineFx.Scheduling.SchedulingService schedulingService = new FluorineFx.Scheduling.SchedulingService();
            AddService(typeof(FluorineFx.Scheduling.ISchedulingService), schedulingService, false);
            schedulingService.Start(null);
            FluorineFx.Messaging.Rtmp.Stream.IBWControlService bwControlService = ObjectFactory.CreateInstance(FluorineConfiguration.Instance.FluorineSettings.BWControlService.Type) as FluorineFx.Messaging.Rtmp.Stream.IBWControlService;
            AddService(typeof(FluorineFx.Messaging.Rtmp.Stream.IBWControlService), bwControlService, false);
            bwControlService.Start(null);
            Init();
		}

		#endregion
	}
}
