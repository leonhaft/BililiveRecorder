﻿using NLog;
using NLog.Targets.Wrappers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BililiveRecorder.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private void CheckUpdate(object sender, StartupEventArgs e)
        {
            logger.Debug($"Starting. FileV:{typeof(App).Assembly.GetName().Version.ToString(4)}, BuildV:{BuildInfo.Version}, Hash:{BuildInfo.HeadSha1}");
            logger.Debug("Environment.CommandLine: " + Environment.CommandLine);
            logger.Debug("Environment.CurrentDirectory: " + Environment.CurrentDirectory);
#if !DEBUG
            Task.Run(RunCheckUpdate);
#endif
        }

        private async Task RunCheckUpdate()
        {
            /*
            try
            {
                if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BILILIVE_RECORDER_DISABLE_UPDATE"))) { return; }
                var envPath = Environment.GetEnvironmentVariable("BILILIVE_RECORDER_OVERWRITE_UPDATE");
                string serverUrl = @"https://soft.danmuji.org/BililiveRecorder/";
                if (!string.IsNullOrWhiteSpace(envPath)) { serverUrl = envPath; }
                using (UpdateManager manager = new UpdateManager(urlOrPath: serverUrl))
                {
                    var update = await manager.CheckForUpdate();
                    if (update.CurrentlyInstalledVersion == null)
                    {
                        logger.Debug("Squirrel 无当前版本");
                    }

                    if (!update.ReleasesToApply.Any())
                    {
                        logger.Info($@"当前运行的是最新版本 ({
                                update.CurrentlyInstalledVersion?.Version?.ToString() ?? "×"
                            }\{
                                typeof(App).Assembly.GetName().Version.ToString(4)
                            })");
                    }
                    else
                    {
                        if (update.CurrentlyInstalledVersion != null
                            && update.FutureReleaseEntry.Version < update.CurrentlyInstalledVersion.Version)
                        {
                            logger.Warn("服务器回滚了一个更新，本地版本比服务器版本高。");
                        }

                        logger.Info($@"服务器最新版本: {
                            update.FutureReleaseEntry?.Version?.ToString() ?? "×"
                        } 当前本地版本: {
                            update.CurrentlyInstalledVersion?.Version?.ToString() ?? "×"
                        }");

                        logger.Info("开始后台下载新版本（不会影响软件运行）");
                        await manager.DownloadReleases(update.ReleasesToApply);
                        logger.Info("新版本下载完成，开始安装（不会影响软件运行）");
                        await manager.ApplyReleases(update);
                        logger.Info("新版本安装完毕，你可以暂时继续使用当前版本。下次启动时会自动启动最新版本。");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "检查更新时出错，如持续出错请联系开发者 rec@danmuji.org");
            }
            */
        }
    }
}