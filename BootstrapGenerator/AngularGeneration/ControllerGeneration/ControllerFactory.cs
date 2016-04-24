namespace BootstrapGenerator.AngularGeneration.ControllerGeneration
{
    public class ControllerFactory
    {
        AAngularView angularView { get; set; }
        public enum ControllerType
        {
            NoHttp, GetHttp, PostHttp, PutHttp, DeleteHttp
        }

        public ControllerFactory(AAngularView angularView)
        {
            this.angularView = angularView;
        }
        public AAngularController createController(ControllerType controllerType)
        {
            switch (controllerType)
            {
                case ControllerType.NoHttp:
                    return new AngularController(angularView);
                default:
                    return new AngularController(angularView);
            }
        }
    }
}
