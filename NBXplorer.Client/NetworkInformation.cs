﻿using NBitcoin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NBXplorer.Configuration;

namespace NBXplorer.Configuration
{
	public class NetworkInformation
	{
		static NetworkInformation()
		{
			_Networks = new Dictionary<string, NetworkInformation>();

			{
				// mainnet
				NetworkInformation info = new NetworkInformation();
				var network = Network.Main;
				info.DefaultDataDirectory = StandardConfiguration.DefaultDataDirectory.GetDirectory("NBXplorer", network.Name);
				info.DefaultConfigurationFile = Path.Combine(info.DefaultDataDirectory, "settings.config");
				info.Network = network;
				info.DefaultExplorerPort = 24444;
				_Networks.Add(network.Name, info);
			}


			{
				// testnet
				NetworkInformation info = new NetworkInformation();
				var network = Network.TestNet;
				info.DefaultDataDirectory = StandardConfiguration.DefaultDataDirectory.GetDirectory("NBXplorer", network.Name);
				info.DefaultConfigurationFile = Path.Combine(info.DefaultDataDirectory, "settings.config");
				info.Network = network;
				info.DefaultExplorerPort = 24445;
				_Networks.Add(network.Name, info);
			}

			{
				// regtest
				NetworkInformation info = new NetworkInformation();
				var network = Network.RegTest;
				info.DefaultDataDirectory = StandardConfiguration.DefaultDataDirectory.GetDirectory("NBXplorer", network.Name);
				info.DefaultConfigurationFile = Path.Combine(info.DefaultDataDirectory, "settings.config");
				info.Network = network;
				info.DefaultExplorerPort = 24446;
				info.IsRegTest = true;
				_Networks.Add(network.Name, info);
			}
		}

		static Dictionary<string, NetworkInformation> _Networks;
		public static NetworkInformation GetNetworkByName(string name)
		{
			if(name == null)
				return null;
			var value = _Networks.TryGet(name);
			if(value != null)
				return value;

			//Maybe alias ?
			var network = Network.GetNetwork(name);
			if(network != null)
			{
				value = _Networks.TryGet(network.Name);
				if(value != null)
					return value;
			}
			return null;
		}
		public Network Network
		{
			get; set;
		}
		public string DefaultConfigurationFile
		{
			get;
			set;
		}
		public string DefaultDataDirectory
		{
			get;
			set;
		}
		public int DefaultExplorerPort
		{
			get;
			internal set;
		}
		public bool IsRegTest
		{
			get;
			set;
		}

		public override string ToString()
		{
			return Network.ToString();
		}

		public static string ToStringAll()
		{
			return string.Join(", ", _Networks.Select(n => n.Key).ToArray());
		}
	}
}