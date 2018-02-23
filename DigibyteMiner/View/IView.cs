using OneMiner.Core.Interfaces;
using OneMiner.View.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMiner.View
{

    interface IView
    {
        void InitializeView();
        void StartView();
        void UpdateMinerList();
        void UpdateSettingsCarousal();
        void UpDateMinerState();
        void ShowHardwareMissingError();
        void RegisterForTimer(OneMinerTimerEvent fun);
        TSQueue<DownloadRequest> DownloadRequestQueue { get; set; }

    }
}
