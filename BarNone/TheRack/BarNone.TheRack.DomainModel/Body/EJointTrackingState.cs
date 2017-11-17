namespace BarNone.TheRack.DomainModel.Body
{
    public enum EJointTrackingState
    {
        //
        // Summary:
        //     The joint data is not tracked and no data is known about this joint.
        NotTracked = 0,
        //
        // Summary:
        //     The joint data is inferred and confidence in the position data is lower than
        //     if it were Tracked.
        Inferred = 1,
        //
        // Summary:
        //     The joint data is being tracked and the data can be trusted.
        Tracked = 2
    }
}
