using DotNetSensorApp.Model;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DotNetSensorApp.ViewModel.Entities
{
    [DataContract]
    public class VMStatus : SimpleViewModelBase
    {
        #region Properties
        private MStatus _model;
        [DataMember]
        public MStatus Model
        {
            get { return _model; }
            set { Set("Model", ref _model, value); }
        }
        #endregion

        #region Constructors
        public VMStatus() { this.Model = new MStatus(); }
        #endregion

        #region StaticMembers
        public static VMStatus Empty
        {
            get
            {
                return new VMStatus();
            }
        }
        #endregion
    }
}
