export default function StatsPanel({
  stats,
  track = [],
  currentIndex = 0
}) {
  const safeStats = stats || {};
  const current = track[currentIndex] || {};
  const currentSensors = current?.sensors || {};
  const sensors = safeStats.maxSensors || {};

  return (
    <div style={styles.panel}>
      <h3>Performance Dashboard</h3>

      <div style={styles.box}>
        <div>
          Max SOG: {Number(safeStats.maxSog ?? 0).toFixed(2)}
        </div>

        <div>
          Max VMG: {Number(safeStats.maxVmg ?? 0).toFixed(2)}
        </div>

        <hr />

        <div>
          Total Distance: {Number(safeStats.totalDistance ?? 0).toFixed(2)} km
        </div>

        <hr />

        <h4>Heart Rate</h4>

        {Object.keys(sensors).length === 0 && (
          <div>No heart rate data</div>
        )}

        {Object.keys(sensors).map((id) => (
          <div key={id}>
            Athlete {id}: {currentSensors[id] || 0} bpm
            {" "} (Max: {sensors[id]})
          </div>
        ))}
      </div>
    </div>
  );
}

// 👇 ADD THIS (missing piece)
const styles = {
  panel: {
    width: 300,
    padding: 15,
    borderLeft: "1px solid #ddd",
    background: "#fff",
    overflowY: "auto",
  },
  box: {
    background: "#f5f5f5",
    padding: 10,
    borderRadius: 8,
  },
};