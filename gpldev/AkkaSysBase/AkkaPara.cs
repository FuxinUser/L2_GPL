﻿using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaSysBase
{
    public class AkkaPara
    {
        /// <summary>
        ///     建立 config
        /// </summary>
        /// <param name="port"> 本地端接口埠號 </param>
        public static Config Config(string port)
        {
            var strConfig = @"
                akka
                {
                    #loglevel = DEBUG
                    #loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
                    actor
                    {
                        provider = remote
                        debug
                        {
                            receive = on      # log any received message
                            autoreceive = on  # log automatically received messages, e.g. PoisonPill
                            lifecycle = on    # log actor lifecycle changes
                            event-stream = on # log subscription changes for Akka.NET event stream
                            unhandled = on    # log unhandled messages sent to actors
                        }
                    }
                    remote 
                    {
                        dot-netty.tcp 
                        {
                            port = {port}
                            hostname = 0.0.0.0
                            public-hostname = localhost
                        }
                    }
                }";
            strConfig = strConfig.Replace("{port}", port);

            return ConfigurationFactory.ParseString(strConfig);
        }
    }
}