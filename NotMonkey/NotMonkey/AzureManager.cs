using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace NotMonkey
{
	public class AzureManager
	{

		private static AzureManager instance;
		private MobileServiceClient client;
        private IMobileServiceTable<NotMonkeyModel> notMonkeyTable;

		private AzureManager()
		{
			this.client = new MobileServiceClient("http://notmonkey.azurewebsites.net");
            this.notMonkeyTable = this.client.GetTable<NotMonkeyModel>();
		}

		public MobileServiceClient AzureClient
		{
			get { return client; }
		}

		public static AzureManager AzureManagerInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new AzureManager();
				}

				return instance;
			}
		}

        public async Task<List<NotMonkeyModel>> GetMonkeyInformation()
		{
            return await this.notMonkeyTable.ToListAsync();
		}

        public async Task PostMonkeyInfo(NotMonkeyModel notMonkeyModel)
		{
            await this.notMonkeyTable.InsertAsync(notMonkeyModel);
		}
	}
}
