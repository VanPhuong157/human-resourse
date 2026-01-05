import React, { useEffect, useRef, useState } from "react";
import { Drawer, Box } from "@mui/material";
import OkrDetail from "../../pages/okr/okrTab/okrDetail/OkrDetail";

const DEFAULT_WIDTH = 720;
const MIN_WIDTH = 400;
const MAX_WIDTH = 1200;

const OkrDetailSidePane = ({ open, onClose, data }) => {
  const [width, setWidth] = useState(DEFAULT_WIDTH);
  const resizerRef = useRef(null);
  const draggingRef = useRef(false);
  const startXRef = useRef(0);
  const startWidthRef = useRef(width);

  useEffect(() => {
    const handleMouseMove = (e) => {
      if (!draggingRef.current) return;
      const dx = startXRef.current - e.clientX;
      const newWidth = Math.min(
        Math.max(startWidthRef.current + dx, MIN_WIDTH),
        MAX_WIDTH
      );
      setWidth(newWidth);
    };

    const handleMouseUp = () => {
      draggingRef.current = false;
      document.body.style.cursor = "";
      window.removeEventListener("mousemove", handleMouseMove);
      window.removeEventListener("mouseup", handleMouseUp);
    };

    if (draggingRef.current) {
      window.addEventListener("mousemove", handleMouseMove);
      window.addEventListener("mouseup", handleMouseUp);
    }

    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
      window.removeEventListener("mouseup", handleMouseUp);
    };
  }, []);

  const startDrag = (e) => {
    draggingRef.current = true;
    startXRef.current = e.clientX;
    startWidthRef.current = width;
    document.body.style.cursor = "ew-resize";
  };

  return (
    <Drawer
      anchor="right"
      open={open}
      onClose={onClose}
      keepMounted
      PaperProps={{
        sx: {
          width: { xs: "100%", md: width },
          borderRadius: { xs: 0, md: "12px 0 0 12px" },
          height: "100dvh",
          display: "flex",
          flexDirection: "column",
          overflow: "hidden",
          position: "relative",
        },
      }}
    >
      {/* Thanh kéo */}
      <Box
        ref={resizerRef}
        onMouseDown={startDrag}
        sx={{
          position: "absolute",
          left: 0,
          top: 0,
          bottom: 0,
          width: "8px",
          cursor: "ew-resize",
          zIndex: 10,
          "&:hover": {
            backgroundColor: "rgba(0,0,0,0.05)",
          },
        }}
      />

      {/* Nội dung */}
      <Box sx={{ flex: 1, overflowY: "auto" }}>
        <OkrDetail data={data} />
      </Box>
    </Drawer>
  );
};

export default OkrDetailSidePane;
