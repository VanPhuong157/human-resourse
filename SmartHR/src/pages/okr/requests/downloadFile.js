// src/pages/okr/requests/downloadFile.js
import path from '../../../api/path'
import { rootApi } from '../../../api/rootApi'

// Ví dụ: base = http://192.168.4.225:5158/api  → origin = http://192.168.4.225:5158
const apiBase   = rootApi?.defaults?.baseURL || ''
const apiOrigin = (() => {
  try { return new URL(apiBase).origin } catch { return apiBase.replace(/\/api\/?$/,'') }
})()
const absApi = (rel) => `${apiBase}/${String(rel || '')}`.replace(/([^:]\/)\/+/g, '$1')

/* Lấy file name từ Content-Disposition (hỗ trợ RFC5987) */
const filenameFromCD = (headers, fallback) => {
  const cd = headers?.['content-disposition'] || headers?.get?.('content-disposition') || ''
  const m = /filename\*=UTF-8''([^;]+)|filename="?([^";]+)"?/i.exec(cd)
  const raw = m?.[1] || m?.[2]
  if (!raw) return fallback
  try { return decodeURIComponent(raw) } catch { return raw || fallback }
}

/* Chuẩn hoá URL tĩnh: /Uploads..., /files/Uploads... → tuyệt đối theo host */
const normalizeUrl = (u) => {
  if (!u) return ''
  const s = String(u).trim()
  if (/^https?:\/\//i.test(s)) return s
  if (s.startsWith('/files/Uploads')) return `${apiOrigin}${s.replace(/^\/files/, '')}`
  if (s.startsWith('/Uploads'))      return `${apiOrigin}${s}`
  return `${apiOrigin}${s.startsWith('/') ? s : `/${s}`}`
}

/* 1) Tải theo fileId (OKR Histories) – y hệt cũ */
export const downloadCommentFile = async (fileId, nameFallback = 'download') => {
  const url = path.okr.downloadCommentFile({ fileId }) // /OkrHistories/download?fileId=...
  const res = await rootApi.get(url, { responseType: 'blob' })
  const filename = filenameFromCD(res.headers, nameFallback)
  const blobUrl  = URL.createObjectURL(res.data)
  const a = document.createElement('a'); a.href = blobUrl; a.download = filename
  document.body.appendChild(a); a.click(); a.remove()
  URL.revokeObjectURL(blobUrl)
}

/* 2) Tải theo downloadUrl (BE trả về sẵn link tĩnh /Uploads/...) */
export const downloadByUrl = async (urlLike, nameFallback = 'download') => {
  const href = normalizeUrl(urlLike)
  const res  = await rootApi.get(href, { responseType: 'blob', baseURL: '' }) // baseURL='' để KHÔNG gắn /api
  const filename = filenameFromCD(res.headers, nameFallback)
  const blobUrl  = URL.createObjectURL(res.data)
  const a = document.createElement('a'); a.href = blobUrl; a.download = filename
  document.body.appendChild(a); a.click(); a.remove()
  URL.revokeObjectURL(blobUrl)
}

/* 3) URL preview inline (ảnh/pdf) cho OKR Histories */
export const previewUrlForCommentFile = (fileId) =>
  absApi(path.okr.downloadCommentFile({ fileId, inline: true }))
