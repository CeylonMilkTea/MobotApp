using Microsoft.Extensions.Configuration;
using MobotApp.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MobotApp.Library
{
	public class MobotApiManager
	{
		private readonly HttpClient _client;
		private readonly IConfiguration _config;
		private readonly int mMaxRetry = 4;// 最大重試次數
		private readonly string connectionName = "API";
		TimeSpan mDelay = TimeSpan.FromSeconds(2); // 初始延遲時間

		public MobotApiManager(IConfiguration config)
		{
			this._config = config;
			_client = new HttpClient();
			_client.Timeout = TimeSpan.FromSeconds(10);
		}

		public async Task<RobotInfo> GetRobotInfo(int Id)
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/robots/{Id}";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					RobotInfo Robot;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						Robot = JsonSerializer.Deserialize<RobotInfo>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return Robot;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<List<RobotInfo>> GetAllRobotInfo()
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/robots";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					List<RobotInfo> Robots;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						Robots = JsonSerializer.Deserialize<List<RobotInfo>>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return Robots;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MissionInfo> MissionCreation(MissionInfo mission)
		{
			int attempt = 0;

			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = "api/v1/missions";

			string url = $"{connectionString}/{functionName}";

			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					HttpResponseMessage response;

					var json = JsonSerializer.Serialize(mission, new JsonSerializerOptions
					{
						PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // 將屬性名稱首字母轉為小寫
					});

					if (string.IsNullOrEmpty(mission.Ref_uuid) == true)
					{
						response = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
					}
					else
					{
						response = await _client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
					}

					MissionInfo lMission;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						lMission = JsonSerializer.Deserialize<MissionInfo>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return lMission;

				}
				catch (HttpRequestException ex)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<List<MissionInfo>> GetOnGoingMission()
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/missions";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					List<MissionInfo> Missions;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						Missions = JsonSerializer.Deserialize<List<MissionInfo>>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return Missions;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MissionInfo> GetMission(int Id)
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/missions{Id}";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					MissionInfo Mission;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						Mission = JsonSerializer.Deserialize<MissionInfo>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return Mission;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MissionInfo> GetMission(string uuid)
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/uuid/missions/{uuid}";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					MissionInfo Mission;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						Mission = JsonSerializer.Deserialize<MissionInfo>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return Mission;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MissionCommand> CancelMission(MissionCommand missionCommand)
		{
			int attempt = 0;

			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = "api/v1/mscmds";

			string url = $"{connectionString}/{functionName}";

			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var json = JsonSerializer.Serialize(missionCommand, new JsonSerializerOptions
					{
						PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // 將屬性名稱首字母轉為小寫
					});

					var response = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

					MissionCommand lMissionCommand;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						lMissionCommand = JsonSerializer.Deserialize<MissionCommand>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return lMissionCommand;

				}
				catch (HttpRequestException ex)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MissionCommand> GetCancelMissionResult(int Id)
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/mscmds/{Id}";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					MissionCommand missionCommand;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						missionCommand = JsonSerializer.Deserialize<MissionCommand>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return missionCommand;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<List<MapInfo>> GetMapInfos()
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/maps";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					List<MapInfo> MapInfos;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						MapInfos = JsonSerializer.Deserialize<List<MapInfo>>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return MapInfos;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<MapDetail> GetMapDetail(string mapName)
		{
			int attempt = 0;
			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = $"api/v1/maps/{mapName}";

			string uri = $"{connectionString}/{functionName}";
			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var response = await _client.GetAsync(uri);

					MapDetail mapDetail;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						mapDetail = JsonSerializer.Deserialize<MapDetail>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return mapDetail;
				}
				catch (HttpRequestException)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

		public async Task<TSPResponse> TSPProblemSolving(TSPRequest request)
		{
			int attempt = 0;

			TimeSpan lDelay = mDelay;

			string connectionString = _config.GetConnectionString(connectionName);

			string functionName = "api/v1/tsp";

			string url = $"{connectionString}/{functionName}";

			while (attempt < mMaxRetry)
			{
				try
				{
					attempt++;

					var json = JsonSerializer.Serialize(request, new JsonSerializerOptions
					{
						PropertyNamingPolicy = JsonNamingPolicy.CamelCase  // 將屬性名稱首字母轉為小寫
					});

					var response = await _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));

					TSPResponse TspResponse;

					if (response.IsSuccessStatusCode)
					{
						var options = new JsonSerializerOptions
						{
							PropertyNameCaseInsensitive = true
						};
						string responseText = await response.Content.ReadAsStringAsync();
						TspResponse = JsonSerializer.Deserialize<TSPResponse>(responseText, options);
					}
					else if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
					{
						throw new HttpRequestException("Service Unavailable");
					}
					else
					{
						throw new Exception(response.ReasonPhrase);
					}
					return TspResponse;

				}
				catch (HttpRequestException ex)
				{
					if (attempt >= mMaxRetry)
					{
						throw; // 達到最大重試次數，終止並拋出異常
					}

					await Task.Delay(lDelay);
					lDelay = TimeSpan.FromSeconds(lDelay.TotalSeconds * 2); // 指數退避（每次等待時間加倍）

				}
				catch (Exception)
				{
					throw;
				}
			}

			throw new Exception("Unexpected Position.");
		}

	}
}
