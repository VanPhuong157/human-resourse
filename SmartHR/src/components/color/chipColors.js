// components/chipColors.js
// ==== Chỉ màu & helper, không dùng React/MUI ====

// Status
export const STATUS_COLORS = {
  Done: {bg: 'rgba(92,0,163,.14)', fg: 'rgb(85,59,105)'}, // tím
  Processing: {bg: 'rgba(0,96,38,.157)', fg: 'rgb(42,83,60)'}, // xanh lá
  Doing: {bg: 'rgba(0,96,38,.157)', fg: 'rgb(42,83,60)'},
  PostPone: {bg: 'rgba(209,156,0,.282)', fg: 'rgb(84,73,47)'}, // vàng
  'To Do': {bg: 'rgba(59,130,246,.20)', fg: 'rgb(30,64,175)'}, // xanh dương
  'Quá Hạn': {bg: 'rgba(206, 24, 0, 0.165)', fg: 'rgb(109, 53, 49)'},
  default: {bg: 'rgba(229,231,235,1)', fg: 'rgb(107,114,128)'},
}

// Priority
export const PRIORITY_COLORS = {
  Low: {bg: 'rgba(92,0,163,.14)', fg: 'rgb(85,59,105)'}, // tím
  High: {bg: 'rgba(0,96,38,.157)', fg: 'rgb(42,83,60)'}, // xanh lá
  Medium: {bg: 'rgba(183, 0, 78, 0.153)', fg: 'rgb(104, 53, 78)'}, // hồng đỏ
  default: {bg: 'rgba(229,231,235,1)', fg: 'rgb(107,114,128)'},
}

// Lấy màu từ map
export const getChipColors = (map, value) => map?.[value] ?? map?.default

// ===== Palette pastel cho TÊN (32 màu) =====
export const PASTEL_NAME_PALETTE = [
  {bg: 'rgba(92,0,163,.14)', fg: 'rgb(85,59,105)'}, // purple
  {bg: 'rgba(0,96,38,.157)', fg: 'rgb(42,83,60)'}, // green
  {bg: 'rgba(59,130,246,.20)', fg: 'rgb(30,64,175)'}, // blue
  {bg: 'rgba(209,156,0,.282)', fg: 'rgb(84,73,47)'}, // amber
  {bg: 'rgba(183,0,78,.153)', fg: 'rgb(104,53,78)'}, // rose
  {bg: 'rgba(14,165,233,.18)', fg: 'rgb(12,74,110)'}, // sky
  {bg: 'rgba(34,197,94,.18)', fg: 'rgb(22,101,52)'}, // emerald
  {bg: 'rgba(245,158,11,.20)', fg: 'rgb(124,45,18)'}, // orange
  {bg: 'rgba(139,92,246,.18)', fg: 'rgb(76,29,149)'}, // violet
  {bg: 'rgba(236,72,153,.18)', fg: 'rgb(131,24,67)'}, // pink
  {bg: 'rgba(6,182,212,.18)', fg: 'rgb(21,94,117)'}, // cyan
  {bg: 'rgba(20,184,166,.18)', fg: 'rgb(19,78,74)'}, // teal
  {bg: 'rgba(132,204,22,.18)', fg: 'rgb(63,98,18)'}, // lime
  {bg: 'rgba(234,179,8,.20)', fg: 'rgb(113,63,18)'}, // yellow
  {bg: 'rgba(99,102,241,.18)', fg: 'rgb(49,46,129)'}, // indigo
  {bg: 'rgba(168,85,247,.18)', fg: 'rgb(107,33,168)'}, // purple
  {bg: 'rgba(59, 201, 159,.18)', fg: 'rgb(2,82,69)'}, // mint
  {bg: 'rgba(96,165,250,.20)', fg: 'rgb(30,58,138)'}, // light-blue
  {bg: 'rgba(74,222,128,.18)', fg: 'rgb(22,101,52)'}, // green-300
  {bg: 'rgba(250,204,21,.20)', fg: 'rgb(120,53,15)'}, // amber-400
  {bg: 'rgba(248,113,113,.18)', fg: 'rgb(127,29,29)'}, // red-400
  {bg: 'rgba(253,186,116,.20)', fg: 'rgb(124,45,18)'}, // orange-300
  {bg: 'rgba(147,197,253,.20)', fg: 'rgb(29,78,216)'}, // blue-300
  {bg: 'rgba(165,180,252,.18)', fg: 'rgb(55,48,163)'}, // indigo-300
  {bg: 'rgba(196,181,253,.18)', fg: 'rgb(76,29,149)'}, // violet-300
  {bg: 'rgba(244,114,182,.18)', fg: 'rgb(131,24,67)'}, // pink-400
  {bg: 'rgba(125,211,252,.18)', fg: 'rgb(7,89,133)'}, // sky-300
  {bg: 'rgba(110,231,183,.18)', fg: 'rgb(6,95,70)'}, // emerald-300
  {bg: 'rgba(187,247,208,.30)', fg: 'rgb(22,101,52)'}, // green-100
  {bg: 'rgba(254,240,138,.30)', fg: 'rgb(113,63,18)'}, // yellow-200
  {bg: 'rgba(148,163,184,.22)', fg: 'rgb(51,65,85)'}, // slate
  {bg: 'rgba(161,161,170,.22)', fg: 'rgb(63,63,70)'}, // zinc
]

// Hash chuỗi → index màu (ổn định)
const _hash = (s = '') => {
  let h = 0
  for (let i = 0; i < s.length; i++) h = (h << 5) - h + s.charCodeAt(i)
  return Math.abs(h)
}

// Màu cho tên người (ổn định, pastel)
export const nameColors = (name = '') => {
  const list = PASTEL_NAME_PALETTE
  const idx = list.length ? _hash(name) % list.length : 0
  return list[idx]
}
