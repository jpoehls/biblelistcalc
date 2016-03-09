<Query Kind="Program" />

void Main()
{
	const bool ALL_ARG = true;
	const string POSITIONS_ARG = "mark 12, gen 42, 1cor 4, 1john 1, eccl 2, ps 40, prov 9, judg 15, isa 38, acts 10";

	var includedLists = new List<ReadingList>();
	var positions = POSITIONS_ARG.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

	var progress = new List<ReadingListProgress>();

	Book book;
	int chapter;
	foreach (var pos in positions)
	{
		if (TryParseBookAndChapter(pos, out book, out chapter))
		{
			var list = FindListByBook(book);
			progress.Add(new ReadingListProgress(list, book, chapter));
		}
	}

	if (ALL_ARG)
	{
		foreach (var list in LISTS.Where(x => !progress.Any(y => y.List == x)).ToList())
		{
			progress.Add(new ReadingListProgress(list, list.Books[0], 1));
		}
	}

	// Sort the progress by the list position.
	progress = progress.OrderBy(x => LISTS.IndexOf(x.List)).ToList();

	// Write list info.
	//	progress.Select(x => new
	//	{
	//		List = x.List.Name,
	//		Days = x.TotalChapters,
	//		Books = string.Join(", ", x.List.Books.Select(y => y.OsisId))
	//	}).Dump("Lists", 1);

	progress.Select(x => new
	{
		List = x.List.Name,
		NextChapter = x.CurrentBook.Name + " " + x.CurrentChapter,
		Progress = GetProgressString(x.CompletedChapters, x.TotalChapters),
		CompletionDate1 = GetEtaDate(x.CompletionDate1),
		CompletionDate2 = GetEtaDate(x.CompletionDate2)
	}).Dump("Progress", 1);
}

#region - Books -

internal class Book
{
	internal Book(string osisId, string name, int order, int chapters)
	{
		OsisId = osisId;
		Name = name;
		Order = order;
		Chapters = chapters;
	}

	public string OsisId { get; }
	public string Name { get; }
	public int Order { get; }
	public int Chapters { get; }

	public override string ToString()
	{
		return Name;
	}
}

internal static readonly Book GEN = new Book("Gen", "Genesis", 1, 50);
internal static readonly Book EXOD = new Book("Exod", "Exodus", 2, 40);
internal static readonly Book LEV = new Book("Lev", "Leviticus", 3, 27);
internal static readonly Book NUM = new Book("Num", "Numbers", 4, 36);
internal static readonly Book DEUT = new Book("Deut", "Deuteronomy", 5, 34);
internal static readonly Book JOSH = new Book("Josh", "Joshua", 6, 24);
internal static readonly Book JUDG = new Book("Judg", "Judges", 7, 21);
internal static readonly Book RUTH = new Book("Ruth", "Ruth", 8, 4);
internal static readonly Book SAM1 = new Book("1Sam", "1 Samuel", 9, 31);
internal static readonly Book SAM2 = new Book("2Sam", "2 Samuel", 10, 24);
internal static readonly Book KGS1 = new Book("1Kgs", "1 Kings", 11, 22);
internal static readonly Book KGS2 = new Book("2Kgs", "2 Kings", 12, 25);
internal static readonly Book CHR1 = new Book("1Chr", "1 Chronicles", 13, 29);
internal static readonly Book CHR2 = new Book("2Chr", "2 Chronicles", 14, 36);
internal static readonly Book EZRA = new Book("Ezra", "Ezra", 15, 10);
internal static readonly Book NEH = new Book("Neh", "Nehemiah", 16, 13);
internal static readonly Book ESTH = new Book("Esth", "Esther", 17, 10);
internal static readonly Book JOB = new Book("Job", "Job", 18, 42);
internal static readonly Book PS = new Book("Ps", "Psalms", 19, 150);
internal static readonly Book PROV = new Book("Prov", "Proverbs", 20, 31);
internal static readonly Book ECCL = new Book("Eccl", "Ecclesiastes", 21, 12);
internal static readonly Book SONG = new Book("Song", "Song of Solomon", 22, 8);
internal static readonly Book ISA = new Book("Isa", "Isaiah", 23, 66);
internal static readonly Book JER = new Book("Jer", "Jeremiah", 24, 52);
internal static readonly Book LAM = new Book("Lam", "Lamentations", 25, 5);
internal static readonly Book EZEK = new Book("Ezek", "Ezekiel", 26, 48);
internal static readonly Book DAN = new Book("Dan", "Daniel", 27, 12);
internal static readonly Book HOS = new Book("Hos", "Hosea", 28, 14);
internal static readonly Book JOEL = new Book("Joel", "Joel", 29, 3);
internal static readonly Book AMOS = new Book("Amos", "Amos", 30, 9);
internal static readonly Book OBAD = new Book("Obad", "Obadiah", 31, 1);
internal static readonly Book JONAH = new Book("Jonah", "Jonah", 32, 4);
internal static readonly Book MIC = new Book("Mic", "Micah", 33, 7);
internal static readonly Book NAH = new Book("Nah", "Nahum", 34, 3);
internal static readonly Book HAB = new Book("Hab", "Habakkuk", 35, 3);
internal static readonly Book ZEPH = new Book("Zeph", "Zephaniah", 36, 3);
internal static readonly Book HAG = new Book("Hag", "Haggai", 37, 2);
internal static readonly Book ZECH = new Book("Zech", "Zechariah", 38, 14);
internal static readonly Book MAL = new Book("Mal", "Malachi", 39, 4);
internal static readonly Book MATT = new Book("Matt", "Matthew", 40, 28);
internal static readonly Book MARK = new Book("Mark", "Mark", 41, 16);
internal static readonly Book LUKE = new Book("Luke", "Luke", 42, 24);
internal static readonly Book JOHN = new Book("John", "John", 43, 21);
internal static readonly Book ACTS = new Book("Acts", "Acts", 44, 28);
internal static readonly Book ROM = new Book("Rom", "Romans", 45, 16);
internal static readonly Book COR1 = new Book("1Cor", "1 Corinthians", 46, 16);
internal static readonly Book COR2 = new Book("2Cor", "2 Corinthians", 47, 13);
internal static readonly Book GAL = new Book("Gal", "Galatians", 48, 6);
internal static readonly Book EPH = new Book("Eph", "Ephesians", 49, 6);
internal static readonly Book PHIL = new Book("Phil", "Philippians", 50, 4);
internal static readonly Book COL = new Book("Col", "Colossians", 51, 4);
internal static readonly Book THESS1 = new Book("1Thess", "1 Thessalonians", 52, 5);
internal static readonly Book THESS2 = new Book("2Thess", "2 Thessalonians", 53, 3);
internal static readonly Book TIM1 = new Book("1Tim", "1 Timothy", 54, 6);
internal static readonly Book TIM2 = new Book("2Tim", "2 Timothy", 55, 4);
internal static readonly Book TITUS = new Book("Titus", "Titus", 56, 3);
internal static readonly Book PHIM = new Book("Phim", "Philemon", 57, 1);
internal static readonly Book HEB = new Book("Heb", "Hebrews", 58, 13);
internal static readonly Book JAS = new Book("Jas", "James", 59, 5);
internal static readonly Book PET1 = new Book("1Pet", "1 Peter", 60, 5);
internal static readonly Book PET2 = new Book("2Pet", "2 Peter", 61, 3);
internal static readonly Book JOHN1 = new Book("1John", "1 John", 62, 5);
internal static readonly Book JOHN2 = new Book("2John", "2 John", 63, 1);
internal static readonly Book JOHN3 = new Book("3John", "3 John", 64, 1);
internal static readonly Book JUDE = new Book("Jude", "Jude", 65, 1);
internal static readonly Book REV = new Book("Rev", "Revelation", 66, 22);

internal static readonly List<Book> BOOKS = new List<Book>()
{
	GEN,
	EXOD,
	LEV,
	NUM,
	DEUT,
	JOSH,
	JUDG,
	RUTH,
	SAM1,
	SAM2,
	KGS1,
	KGS2,
	CHR1,
	CHR2,
	EZRA,
	NEH,
	ESTH,
	JOB,
	PS,
	PROV,
	ECCL,
	SONG,
	ISA,
	JER,
	LAM,
	EZEK,
	DAN,
	HOS,
	JOEL,
	AMOS,
	OBAD,
	JONAH,
	MIC,
	NAH,
	HAB,
	ZEPH,
	HAG,
	ZECH,
	MAL,
	MATT,
	MARK,
	LUKE,
	JOHN,
	ACTS,
	ROM,
	COR1,
	COR2,
	GAL,
	EPH,
	PHIL,
	COL,
	THESS1,
	THESS2,
	TIM1,
	TIM2,
	TITUS,
	PHIM,
	HEB,
	JAS,
	PET1,
	PET2,
	JOHN1,
	JOHN2,
	JOHN3,
	JUDE,
	REV
};

#endregion

#region - Lists -

internal class ReadingList
{
	internal ReadingList(string name, params Book[] books)
	{
		Name = name;
		Books = new List<Book>(books);
	}

	public string Name { get; }
	public List<Book> Books { get; }

	public override string ToString()
	{
		return Name;
	}
}

internal static readonly List<ReadingList> LISTS = new List<ReadingList>
{
	new ReadingList("List 1", MATT, MARK, LUKE, JOHN),
	new ReadingList("List 2", GEN, EXOD, LEV, NUM, DEUT),
	new ReadingList("List 3", ROM, COR1, COR2, GAL, EPH, PHIL, COL, HEB),
	new ReadingList("List 4", THESS1, THESS2, TIM1, TIM2, TITUS, PHIM, JAS, PET1, PET2, JOHN1, JOHN2, JOHN3, JUDE, REV),
	new ReadingList("List 5", JOB, ECCL, SONG),
	new ReadingList("List 6", PS),
	new ReadingList("List 7", PROV),
	new ReadingList("List 8", JOSH, JUDG, RUTH, SAM1, SAM2, KGS1, KGS2, CHR1, CHR2, EZRA, NEH, ESTH),
	new ReadingList("List 9", ISA, JER, LAM, EZEK, DAN, HOS, JOEL, AMOS, OBAD, JONAH, MIC, NAH, HAB, ZEPH, HAG, ZECH, MAL),
	new ReadingList("List 10", ACTS),
};

#endregion

internal class ReadingListProgress
{
	internal ReadingListProgress(ReadingList list, Book book, int chapter)
	{
		List = list;
		CurrentBook = book;
		CurrentChapter = chapter;

		TotalChapters = List.Books.Sum(x => x.Chapters);
		CompletedChapters = list.Books.Take(list.Books.IndexOf(book)).Sum(x => x.Chapters) + chapter - 1;

		CompletionDate1 = DateTime.Now.Date.AddDays(TotalChapters - CompletedChapters);
		CompletionDate2 = CompletionDate1.AddDays(TotalChapters);
	}

	public ReadingList List { get; }
	public Book CurrentBook { get; }
	public int CurrentChapter { get; }

	public int TotalChapters { get; }
	public int CompletedChapters { get; }

	public DateTime CompletionDate1 { get; }
	public DateTime CompletionDate2 { get; }
}

string GetEtaDate(DateTime date)
{
	var daysLeft = date.Subtract(DateTime.Now.Date).TotalDays;
	var plural = daysLeft == 1 ? "" : "s";
	return date.ToString("MMM d, yyyy") + " (" + daysLeft + " day" + plural + ")";
}

string GetProgressString(int completed, int total)
{
	var percent = (Convert.ToDecimal(completed) / total) * 100;
	return percent.ToString("N0") + "% (" + completed + "/" + total + ")";
}

ReadingList FindListByBook(Book book)
{
	return LISTS.First(x => x.Books.Contains(book));
}

bool TryParseBookAndChapter(string position, out Book book, out int chapter)
{
	var parts = position.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
	var bookAbbr = parts[0];
	chapter = -1;
	book = BOOKS.FirstOrDefault(x => x.OsisId.Equals(bookAbbr, StringComparison.OrdinalIgnoreCase));
	if (parts.Length == 2)
	{
		int ichapter;
		if (int.TryParse(parts[1], out ichapter))
		{
			chapter = ichapter;
		}
	}

	return book != null && chapter > -1;
}

string GetListDesc(ReadingList list)
{
	var days = list.Books.Sum(x => x.Chapters);
	return $"{list.Name} ({days} days): {string.Join(", ", list.Books.Select(x => x.OsisId))}";
}