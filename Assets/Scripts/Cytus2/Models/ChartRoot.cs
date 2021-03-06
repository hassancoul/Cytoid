﻿using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Cytus2.Models
{
    [System.Serializable]
    public class ChartRoot
    {
        public int format_version;
        public double time_base;

        public float start_offset_time;
        public List<ChartTempo> tempo_list = new List<ChartTempo>();
        public List<ChartPage> page_list = new List<ChartPage>();
        public List<ChartNote> note_list = new List<ChartNote>();
        public List<ChartEventOrder> event_order_list = new List<ChartEventOrder>();
        public List<ChartAnimation> animation_list = new List<ChartAnimation>();

        public double music_offset;
        public double size = 1.0;
        public double opacity = 1.0;
        public string ring_color;
        public string[] fill_colors = new string[10];
    }

    [System.Serializable]
    public class ChartPage
    {
        public double start_tick;
        public double end_tick;
        public int scan_line_direction;

        public float start_time;
        public float end_time;
        public float actual_start_tick;
        public float actual_start_time;

        public float Duration
        {
            get { return end_time - start_time; }
        }
    }

    [System.Serializable]
    public class ChartTempo
    {
        public double tick;
        public long value;
    }

    [System.Serializable]
    public class ChartEvent
    {
        public int type;
        public string args;
    }

    [System.Serializable]
    public class ChartEventOrder
    {
        public int tick;
        public float time;
        public List<ChartEvent> event_list;
    }

    [System.Serializable]
    public class ChartAnimation
    {
        public int tick;
        public float time;
        public int type;
        public string new_text;
        public float transition_time;
    }

    [System.Serializable]
    public class ChartNote
    {
        public int page_index;
        public int type;
        public int id;
        public double tick;
        public double x;
        public bool has_sibling;
        public double hold_tick;
        public int next_id;
        public bool is_forward;

        public double approach_rate = 1.0;
        public double size = double.MinValue;
        public string ring_color = null;
        [JsonIgnore] public Color ParsedRingColor;
        public string fill_color = null;
        [JsonIgnore] public Color ParsedFillColor;
        public double opacity = double.MinValue;

        public float start_time;
        public float end_time;
        public Vector3 position;
        public Vector3 end_position;
        public float holdlength;
        public float speed;
        public float intro_time;
        public int direction;
        public float rotation;
        public float tint;
        public float nextdraglinestarttime;
        public float nextdraglinestoptime;

        public float Duration
        {
            get { return end_time - start_time; }
        }
    }
}