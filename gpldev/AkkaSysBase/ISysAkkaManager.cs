using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaSysBase
{
    public interface ISysAkkaManager
    {
        ActorSystem ActorSystem { get; }

        IActorRef CreateActor<T>() where T : ActorBase;

        IActorRef CreateChildActor<T>(IUntypedActorContext context) where T : ActorBase;

        IActorRef GetActor(string actName);

        ActorSelection GetActorSelection(string actorPath);
    }
}
