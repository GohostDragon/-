﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DummyClient
{
    class SessionManager
    {
        static SessionManager _session = new SessionManager();
        public static SessionManager Instance { get { return _session; } }

        List<ServerSession> _sessions = new List<ServerSession>();
        object _lock = new object();

        public void SendForEach()
        {
            lock (_lock)
            {
                foreach(ServerSession session in _sessions)
                {
                    C_Chat charPacket = new C_Chat();
                    charPacket.chat = $"Hello Server !";
                    ArraySegment<byte> segment = charPacket.Write();

                    session.Send(segment);
                }
            }
        }

        public ServerSession Generate()
        {
            lock(_lock)
            {
                ServerSession session = new ServerSession();
                _sessions.Add(session);
                return session;
            }
        }

    }
}
