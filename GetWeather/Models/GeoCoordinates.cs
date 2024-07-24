using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetWeather.Controllers;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GetWeather.Models;

public class GeoCoordinates
{
    [JsonProperty("GeoData")]
    public GeoDatum[]? GeoData { get; set; }
    
}

public class GeoDatum
{
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("LocalNames")]
    public Local_Names? LocalNames { get; set; }

    [JsonProperty("Lat")]
    public float Lat { get; set; }

    [JsonProperty("Lon")]
    public float Lon { get; set; }

    [JsonProperty("Country")]
    public string? Country { get; set; }

    [JsonProperty("State")]
    public string? State { get; set; }
}

public class Local_Names
{
    [JsonProperty("ms")]
    public string? Ms { get; set; }

    [JsonProperty("gu")]
    public string? Gu { get; set; }

    [JsonProperty("_is")]
    public string? Is { get; set; }

    [JsonProperty("wa")]
    public string? Wa { get; set; }

    [JsonProperty("mg")]
    public string? Mg { get; set; }

    [JsonProperty("gl")]
    public string? Gl { get; set; }

    [JsonProperty("om")]
    public string? Om { get; set; }

    [JsonProperty("ku")]
    public string? Ku { get; set; }

    [JsonProperty("tw")]
    public string? Tw { get; set; }

    [JsonProperty("mk")]
    public string? Mk { get; set; }

    [JsonProperty("ee")]
    public string? Ee { get; set; }

    [JsonProperty("fj")]
    public string? Fj { get; set; }

    [JsonProperty("gd")]
    public string? Gd { get; set; }

    [JsonProperty("ky")]
    public string? Ky { get; set; }

    [JsonProperty("yo")]
    public string? Yo { get; set; }

    [JsonProperty("zu")]
    public string? Zu { get; set; }

    [JsonProperty("bg")]
    public string? Bg { get; set; }

    [JsonProperty("tk")]
    public string? Tk { get; set; }

    [JsonProperty("co")]
    public string? Co { get; set; }

    [JsonProperty("sh")]
    public string? Sh { get; set; }

    [JsonProperty("de")]
    public string? De { get; set; }

    [JsonProperty("kl")]
    public string? Kl { get; set; }

    [JsonProperty("bi")]
    public string? Bi { get; set; }

    [JsonProperty("km")]
    public string? Km { get; set; }

    [JsonProperty("lt")]
    public string? Lt { get; set; }

    [JsonProperty("fi")]
    public string? Fi { get; set; }

    [JsonProperty("fy")]
    public string? Fy { get; set; }

    [JsonProperty("ba")]
    public string? Ba { get; set; }

    [JsonProperty("sc")]
    public string? Sc { get; set; }

    [JsonProperty("feature_name")]
    public string? FeatureName { get; set; }

    [JsonProperty("ja")]
    public string? Ja { get; set; }

    [JsonProperty("am")]
    public string? Am { get; set; }

    [JsonProperty("sk")]
    public string? Sk { get; set; }

    [JsonProperty("mr")]
    public string? Mr { get; set; }

    [JsonProperty("es")]
    public string? Es { get; set; }

    [JsonProperty("sq")]
    public string? Sq { get; set; }

    [JsonProperty("te")]
    public string? Te { get; set; }

    [JsonProperty("br")]
    public string? Br { get; set; }

    [JsonProperty("uz")]
    public string? Uz { get; set; }

    [JsonProperty("da")]
    public string? Da { get; set; }

    [JsonProperty("sw")]
    public string? Sw { get; set; }

    [JsonProperty("fa")]
    public string? Fa { get; set; }

    [JsonProperty("sr")]
    public string? Sr { get; set; }

    [JsonProperty("cu")]
    public string? Cu { get; set; }

    [JsonProperty("ln")]
    public string? Ln { get; set; }

    [JsonProperty("na")]
    public string? Na { get; set; }

    [JsonProperty("wo")]
    public string? Wo { get; set; }

    [JsonProperty("ig")]
    public string? Ig { get; set; }

    [JsonProperty("to")]
    public string? To { get; set; }

    [JsonProperty("ta")]
    public string? Ta { get; set; }

    [JsonProperty("mt")]
    public string? Mt { get; set; }

    [JsonProperty("ar")]
    public string? Ar { get; set; }

    [JsonProperty("su")]
    public string? Su { get; set; }

    [JsonProperty("ab")]
    public string? Ab { get; set; }

    [JsonProperty("ps")]
    public string? Ps { get; set; }

    [JsonProperty("bm")]
    public string? Bm { get; set; }

    [JsonProperty("mi")]
    public string? Mi { get; set; }

    [JsonProperty("kn")]
    public string? Kn { get; set; }

    [JsonProperty("kv")]
    public string? Kv { get; set; }

    [JsonProperty("os")]
    public string? Os { get; set; }

    [JsonProperty("bn")]
    public string? Bn { get; set; }

    [JsonProperty("li")]
    public string? Li { get; set; }

    [JsonProperty("vi")]
    public string? Vi { get; set; }

    [JsonProperty("zh")]
    public string? Zh { get; set; }

    [JsonProperty("eo")]
    public string? Eo { get; set; }

    [JsonProperty("ha")]
    public string? Ha { get; set; }

    [JsonProperty("tt")]
    public string? Tt { get; set; }

    [JsonProperty("lb")]
    public string? Lb { get; set; }

    [JsonProperty("ce")]
    public string? Ce { get; set; }

    [JsonProperty("hu")]
    public string? Hu { get; set; }

    [JsonProperty("it")]
    public string? It { get; set; }

    [JsonProperty("tl")]
    public string? Tl { get; set; }

    [JsonProperty("pl")]
    public string? Pl { get; set; }

    [JsonProperty("sm")]
    public string? Sm { get; set; }

    [JsonProperty("en")]
    public string? En { get; set; }

    [JsonProperty("vo")]
    public string? Vo { get; set; }

    [JsonProperty("el")]
    public string? El { get; set; }

    [JsonProperty("sn")]
    public string? Sn { get; set; }

    [JsonProperty("fr")]
    public string? Fr { get; set; }

    [JsonProperty("cs")]
    public string? Cs { get; set; }

    [JsonProperty("io")]
    public string? Io { get; set; }

    [JsonProperty("hi")]
    public string? Hi { get; set; }

    [JsonProperty("et")]
    public string? Et { get; set; }

    [JsonProperty("pa")]
    public string? Pa { get; set; }

    [JsonProperty("av")]
    public string? Av { get; set; }

    [JsonProperty("ko")]
    public string? Ko { get; set; }

    [JsonProperty("bh")]
    public string? Bh { get; set; }

    [JsonProperty("yi")]
    public string? Yi { get; set; }

    [JsonProperty("sa")]
    public string? Sa { get; set; }

    [JsonProperty("sl")]
    public string? Sl { get; set; }

    [JsonProperty("hr")]
    public string? Hr { get; set; }

    [JsonProperty("si")]
    public string? Si { get; set; }

    [JsonProperty("so")]
    public string? So { get; set; }

    [JsonProperty("gn")]
    public string? Gn { get; set; }

    [JsonProperty("ay")]
    public string? Ay { get; set; }

    [JsonProperty("se")]
    public string? Se { get; set; }

    [JsonProperty("sd")]
    public string? Sd { get; set; }

    [JsonProperty("af")]
    public string? Af { get; set; }

    [JsonProperty("ga")]
    public string? Ga { get; set; }

    [JsonProperty("or")]
    public string? Or { get; set; }

    [JsonProperty("ia")]
    public string? Ia { get; set; }

    [JsonProperty("ie")]
    public string? Ie { get; set; }

    [JsonProperty("ug")]
    public string? Ug { get; set; }

    [JsonProperty("nl")]
    public string? Nl { get; set; }

    [JsonProperty("gv")]
    public string? Gv { get; set; }

    [JsonProperty("qu")]
    public string? Qu { get; set; }

    [JsonProperty("be")]
    public string? Be { get; set; }

    [JsonProperty("an")]
    public string? An { get; set; }

    [JsonProperty("fo")]
    public string? Fo { get; set; }

    [JsonProperty("hy")]
    public string? Hy { get; set; }

    [JsonProperty("nv")]
    public string? Nv { get; set; }

    [JsonProperty("bo")]
    public string? Bo { get; set; }

    [JsonProperty("ascii")]
    public string? Ascii { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("lv")]
    public string? Lv { get; set; }

    [JsonProperty("ca")]
    public string? Ca { get; set; }

    [JsonProperty("no")]
    public string? No { get; set; }

    [JsonProperty("nn")]
    public string? Nn { get; set; }

    [JsonProperty("ml")]
    public string? Ml { get; set; }

    [JsonProperty("my")]
    public string? My { get; set; }

    [JsonProperty("ne")]
    public string? Ne { get; set; }

    [JsonProperty("he")]
    public string? He { get; set; }

    [JsonProperty("cy")]
    public string? Cy { get; set; }

    [JsonProperty("lo")]
    public string? Lo { get; set; }

    [JsonProperty("jv")]
    public string? Jv { get; set; }

    [JsonProperty("sv")]
    public string? Sv { get; set; }

    [JsonProperty("mn")]
    public string? Mn { get; set; }

    [JsonProperty("tg")]
    public string? Tg { get; set; }

    [JsonProperty("kw")]
    public string? Kw { get; set; }

    [JsonProperty("cv")]
    public string? Cv { get; set; }

    [JsonProperty("az")]
    public string? Az { get; set; }

    [JsonProperty("oc")]
    public string? Oc { get; set; }

    [JsonProperty("th")]
    public string? Th { get; set; }

    [JsonProperty("ru")]
    public string? Ru { get; set; }

    [JsonProperty("ny")]
    public string? Ny { get; set; }

    [JsonProperty("bs")]
    public string? Bs { get; set; }

    [JsonProperty("st")]
    public string? St { get; set; }

    [JsonProperty("ro")]
    public string? Ro { get; set; }

    [JsonProperty("rm")]
    public string? Rm { get; set; }

    [JsonProperty("ff")]
    public string? Ff { get; set; }

    [JsonProperty("kk")]
    public string? Kk { get; set; }

    [JsonProperty("uk")]
    public string? Uk { get; set; }

    [JsonProperty("pt")]
    public string? Pt { get; set; }

    [JsonProperty("tr")]
    public string? Tr { get; set; }

    [JsonProperty("eu")]
    public string? Eu { get; set; }

    [JsonProperty("ht")]
    public string? Ht { get; set; }

    [JsonProperty("ka")]
    public string? Ka { get; set; }

    [JsonProperty("ur")]
    public string? Ur { get; set; }

    [JsonProperty("oj")]
    public string? Oj { get; set; }

    [JsonProperty("cr")]
    public string? Cr { get; set; }

    [JsonProperty("iu")]
    public string? Iu { get; set; }
}