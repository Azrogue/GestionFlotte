using System;
namespace FlotteVoiture
{

    public interface IMaintenable
    {
        void EffectuerMaintenance();
        event EventHandler MaintenanceDue;
    }

}

