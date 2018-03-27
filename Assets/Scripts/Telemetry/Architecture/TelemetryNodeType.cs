
public class TelemetryNodeType
{
    public string Value { get; set; }

    private TelemetryNodeType(string value) { Value = value; }

    public static TelemetryNodeType Atomic { get { return new TelemetryNodeType("Atomic"); } }
    public static TelemetryNodeType SingleEvent { get { return new TelemetryNodeType("Single Event"); } }
    public static TelemetryNodeType ChainEvent { get { return new TelemetryNodeType("Chain Event"); } }
}
