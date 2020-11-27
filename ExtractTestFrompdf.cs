public string ExtractText()
        {
            string result = "";
            PDFPage page = GetCurrentPage();
            PDFRect bbox = new PDFRect();
            TextExtractor.Line line;
            TextExtractor.Word word;
            TextExtractor txt = new TextExtractor();
            JArray wordlist = new JArray();
            txt.Begin(page);
            for (line = txt.GetFirstLine(); line.IsValid(); line = line.GetNextLine())
            {
                if (line.GetNumWords() == 0)
                {
                    continue;
                }
                int flow_id, para_id;
                flow_id = line.GetFlowID();
                para_id = line.GetParagraphID();
                for (word = line.GetFirstWord(); word.IsValid(); word = word.GetNextWord())
                {
                    int sz = word.GetStringLen();
                    if (sz == 0) continue;
                    bbox = word.GetBBox();
                    JObject wordobj = new JObject();
                    JObject coordinate = new JObject();
                    string text = word.GetString();
                    coordinate["x1"] = bbox.x1;
                    coordinate["y1"] = bbox.y1;
                    coordinate["x2"] = bbox.x2;
                    coordinate["y2"] = bbox.y2;
                    wordobj["coordinate"] = coordinate;
                    wordobj["text"] = text;
                    wordobj["flow_id"] = flow_id;
                    wordobj["para_id"] = para_id;
                    wordlist.Add(wordobj);
                }
            }
            if (wordlist.Count > 0)
            {
                result = wordlist.ToString();
            }
            return result;
        }