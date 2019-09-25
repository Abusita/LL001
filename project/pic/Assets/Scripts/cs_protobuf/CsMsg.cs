// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: cs_msg.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace CsProtobuf {

  /// <summary>Holder for reflection information generated from cs_msg.proto</summary>
  public static partial class CsMsgReflection {

    #region Descriptor
    /// <summary>File descriptor for cs_msg.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CsMsgReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cgxjc19tc2cucHJvdG8SC2NzX3Byb3RvYnVmGh1nb29nbGUvcHJvdG9idWYv",
            "Y3NfZW51bS5wcm90byKvAQoHTXNnUGFjaxIlCgdtc2dUeXBlGAEgASgOMhQu",
            "Y3NfcHJvdG9idWYuTXNnVHlwZRImCgdtc2dGcm9tGAIgASgOMhUuY3NfcHJv",
            "dG9idWYuUGxheWVySUQSJAoFbXNnVG8YAyABKA4yFS5jc19wcm90b2J1Zi5Q",
            "bGF5ZXJJRBIvCgxpbml0SXRlbVBhY2sYBCABKAsyGS5jc19wcm90b2J1Zi5J",
            "bml0SXRlbVBhY2sibgoHQ2FyZE1zZxINCgVtYXhIcBgBIAEoAhILCgNhdGsY",
            "AiABKAISCwoDZGVmGAMgASgCEg0KBXNwZWVkGAQgASgCEgoKAmlkGAUgASgF",
            "Eg4KBmlzQm9ybhgGIAEoCBIPCgdib3JuUG9zGAcgASgFIpIBCghDYW1wSW5m",
            "bxInCghwbGF5ZXJJRBgBIAEoDjIVLmNzX3Byb3RvYnVmLlBsYXllcklEEiAK",
            "BGNhbXAYAiABKA4yEi5jc19wcm90b2J1Zi5DYW1wcxISCgppdGVtc0NvdW50",
            "GAMgASgFEicKCWNhcmRJdGVtcxgEIAMoCzIULmNzX3Byb3RvYnVmLkNhcmRN",
            "c2ciOAoMSW5pdEl0ZW1QYWNrEigKCWNhbXBJbmZvcxgBIAMoCzIVLmNzX3By",
            "b3RvYnVmLkNhbXBJbmZvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::CsProtobuf.CsEnumReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::CsProtobuf.MsgPack), global::CsProtobuf.MsgPack.Parser, new[]{ "MsgType", "MsgFrom", "MsgTo", "InitItemPack" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CsProtobuf.CardMsg), global::CsProtobuf.CardMsg.Parser, new[]{ "MaxHp", "Atk", "Def", "Speed", "Id", "IsBorn", "BornPos" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CsProtobuf.CampInfo), global::CsProtobuf.CampInfo.Parser, new[]{ "PlayerID", "Camp", "ItemsCount", "CardItems" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::CsProtobuf.InitItemPack), global::CsProtobuf.InitItemPack.Parser, new[]{ "CampInfos" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class MsgPack : pb::IMessage<MsgPack> {
    private static readonly pb::MessageParser<MsgPack> _parser = new pb::MessageParser<MsgPack>(() => new MsgPack());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<MsgPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CsProtobuf.CsMsgReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgPack(MsgPack other) : this() {
      msgType_ = other.msgType_;
      msgFrom_ = other.msgFrom_;
      msgTo_ = other.msgTo_;
      InitItemPack = other.initItemPack_ != null ? other.InitItemPack.Clone() : null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public MsgPack Clone() {
      return new MsgPack(this);
    }

    /// <summary>Field number for the "msgType" field.</summary>
    public const int MsgTypeFieldNumber = 1;
    private global::CsProtobuf.MsgType msgType_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.MsgType MsgType {
      get { return msgType_; }
      set {
        msgType_ = value;
      }
    }

    /// <summary>Field number for the "msgFrom" field.</summary>
    public const int MsgFromFieldNumber = 2;
    private global::CsProtobuf.PlayerID msgFrom_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.PlayerID MsgFrom {
      get { return msgFrom_; }
      set {
        msgFrom_ = value;
      }
    }

    /// <summary>Field number for the "msgTo" field.</summary>
    public const int MsgToFieldNumber = 3;
    private global::CsProtobuf.PlayerID msgTo_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.PlayerID MsgTo {
      get { return msgTo_; }
      set {
        msgTo_ = value;
      }
    }

    /// <summary>Field number for the "initItemPack" field.</summary>
    public const int InitItemPackFieldNumber = 4;
    private global::CsProtobuf.InitItemPack initItemPack_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.InitItemPack InitItemPack {
      get { return initItemPack_; }
      set {
        initItemPack_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as MsgPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(MsgPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MsgType != other.MsgType) return false;
      if (MsgFrom != other.MsgFrom) return false;
      if (MsgTo != other.MsgTo) return false;
      if (!object.Equals(InitItemPack, other.InitItemPack)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (MsgType != 0) hash ^= MsgType.GetHashCode();
      if (MsgFrom != 0) hash ^= MsgFrom.GetHashCode();
      if (MsgTo != 0) hash ^= MsgTo.GetHashCode();
      if (initItemPack_ != null) hash ^= InitItemPack.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (MsgType != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) MsgType);
      }
      if (MsgFrom != 0) {
        output.WriteRawTag(16);
        output.WriteEnum((int) MsgFrom);
      }
      if (MsgTo != 0) {
        output.WriteRawTag(24);
        output.WriteEnum((int) MsgTo);
      }
      if (initItemPack_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(InitItemPack);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (MsgType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgType);
      }
      if (MsgFrom != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgFrom);
      }
      if (MsgTo != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) MsgTo);
      }
      if (initItemPack_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(InitItemPack);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(MsgPack other) {
      if (other == null) {
        return;
      }
      if (other.MsgType != 0) {
        MsgType = other.MsgType;
      }
      if (other.MsgFrom != 0) {
        MsgFrom = other.MsgFrom;
      }
      if (other.MsgTo != 0) {
        MsgTo = other.MsgTo;
      }
      if (other.initItemPack_ != null) {
        if (initItemPack_ == null) {
          initItemPack_ = new global::CsProtobuf.InitItemPack();
        }
        InitItemPack.MergeFrom(other.InitItemPack);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            msgType_ = (global::CsProtobuf.MsgType) input.ReadEnum();
            break;
          }
          case 16: {
            msgFrom_ = (global::CsProtobuf.PlayerID) input.ReadEnum();
            break;
          }
          case 24: {
            msgTo_ = (global::CsProtobuf.PlayerID) input.ReadEnum();
            break;
          }
          case 34: {
            if (initItemPack_ == null) {
              initItemPack_ = new global::CsProtobuf.InitItemPack();
            }
            input.ReadMessage(initItemPack_);
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///卡牌
  /// </summary>
  public sealed partial class CardMsg : pb::IMessage<CardMsg> {
    private static readonly pb::MessageParser<CardMsg> _parser = new pb::MessageParser<CardMsg>(() => new CardMsg());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CardMsg> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CsProtobuf.CsMsgReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CardMsg() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CardMsg(CardMsg other) : this() {
      maxHp_ = other.maxHp_;
      atk_ = other.atk_;
      def_ = other.def_;
      speed_ = other.speed_;
      id_ = other.id_;
      isBorn_ = other.isBorn_;
      bornPos_ = other.bornPos_;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CardMsg Clone() {
      return new CardMsg(this);
    }

    /// <summary>Field number for the "maxHp" field.</summary>
    public const int MaxHpFieldNumber = 1;
    private float maxHp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float MaxHp {
      get { return maxHp_; }
      set {
        maxHp_ = value;
      }
    }

    /// <summary>Field number for the "atk" field.</summary>
    public const int AtkFieldNumber = 2;
    private float atk_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Atk {
      get { return atk_; }
      set {
        atk_ = value;
      }
    }

    /// <summary>Field number for the "def" field.</summary>
    public const int DefFieldNumber = 3;
    private float def_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Def {
      get { return def_; }
      set {
        def_ = value;
      }
    }

    /// <summary>Field number for the "speed" field.</summary>
    public const int SpeedFieldNumber = 4;
    private float speed_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Speed {
      get { return speed_; }
      set {
        speed_ = value;
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 5;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "isBorn" field.</summary>
    public const int IsBornFieldNumber = 6;
    private bool isBorn_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsBorn {
      get { return isBorn_; }
      set {
        isBorn_ = value;
      }
    }

    /// <summary>Field number for the "bornPos" field.</summary>
    public const int BornPosFieldNumber = 7;
    private int bornPos_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int BornPos {
      get { return bornPos_; }
      set {
        bornPos_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CardMsg);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CardMsg other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MaxHp != other.MaxHp) return false;
      if (Atk != other.Atk) return false;
      if (Def != other.Def) return false;
      if (Speed != other.Speed) return false;
      if (Id != other.Id) return false;
      if (IsBorn != other.IsBorn) return false;
      if (BornPos != other.BornPos) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (MaxHp != 0F) hash ^= MaxHp.GetHashCode();
      if (Atk != 0F) hash ^= Atk.GetHashCode();
      if (Def != 0F) hash ^= Def.GetHashCode();
      if (Speed != 0F) hash ^= Speed.GetHashCode();
      if (Id != 0) hash ^= Id.GetHashCode();
      if (IsBorn != false) hash ^= IsBorn.GetHashCode();
      if (BornPos != 0) hash ^= BornPos.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (MaxHp != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(MaxHp);
      }
      if (Atk != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Atk);
      }
      if (Def != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Def);
      }
      if (Speed != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Speed);
      }
      if (Id != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(Id);
      }
      if (IsBorn != false) {
        output.WriteRawTag(48);
        output.WriteBool(IsBorn);
      }
      if (BornPos != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(BornPos);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (MaxHp != 0F) {
        size += 1 + 4;
      }
      if (Atk != 0F) {
        size += 1 + 4;
      }
      if (Def != 0F) {
        size += 1 + 4;
      }
      if (Speed != 0F) {
        size += 1 + 4;
      }
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (IsBorn != false) {
        size += 1 + 1;
      }
      if (BornPos != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(BornPos);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CardMsg other) {
      if (other == null) {
        return;
      }
      if (other.MaxHp != 0F) {
        MaxHp = other.MaxHp;
      }
      if (other.Atk != 0F) {
        Atk = other.Atk;
      }
      if (other.Def != 0F) {
        Def = other.Def;
      }
      if (other.Speed != 0F) {
        Speed = other.Speed;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.IsBorn != false) {
        IsBorn = other.IsBorn;
      }
      if (other.BornPos != 0) {
        BornPos = other.BornPos;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 13: {
            MaxHp = input.ReadFloat();
            break;
          }
          case 21: {
            Atk = input.ReadFloat();
            break;
          }
          case 29: {
            Def = input.ReadFloat();
            break;
          }
          case 37: {
            Speed = input.ReadFloat();
            break;
          }
          case 40: {
            Id = input.ReadInt32();
            break;
          }
          case 48: {
            IsBorn = input.ReadBool();
            break;
          }
          case 56: {
            BornPos = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  /// <summary>
  ///初始化消息包
  /// </summary>
  public sealed partial class CampInfo : pb::IMessage<CampInfo> {
    private static readonly pb::MessageParser<CampInfo> _parser = new pb::MessageParser<CampInfo>(() => new CampInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CampInfo> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CsProtobuf.CsMsgReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CampInfo() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CampInfo(CampInfo other) : this() {
      playerID_ = other.playerID_;
      camp_ = other.camp_;
      itemsCount_ = other.itemsCount_;
      cardItems_ = other.cardItems_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CampInfo Clone() {
      return new CampInfo(this);
    }

    /// <summary>Field number for the "playerID" field.</summary>
    public const int PlayerIDFieldNumber = 1;
    private global::CsProtobuf.PlayerID playerID_ = 0;
    /// <summary>
    ///身份
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.PlayerID PlayerID {
      get { return playerID_; }
      set {
        playerID_ = value;
      }
    }

    /// <summary>Field number for the "camp" field.</summary>
    public const int CampFieldNumber = 2;
    private global::CsProtobuf.Camps camp_ = 0;
    /// <summary>
    ///对战阵营
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::CsProtobuf.Camps Camp {
      get { return camp_; }
      set {
        camp_ = value;
      }
    }

    /// <summary>Field number for the "itemsCount" field.</summary>
    public const int ItemsCountFieldNumber = 3;
    private int itemsCount_;
    /// <summary>
    ///卡牌数量
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ItemsCount {
      get { return itemsCount_; }
      set {
        itemsCount_ = value;
      }
    }

    /// <summary>Field number for the "cardItems" field.</summary>
    public const int CardItemsFieldNumber = 4;
    private static readonly pb::FieldCodec<global::CsProtobuf.CardMsg> _repeated_cardItems_codec
        = pb::FieldCodec.ForMessage(34, global::CsProtobuf.CardMsg.Parser);
    private readonly pbc::RepeatedField<global::CsProtobuf.CardMsg> cardItems_ = new pbc::RepeatedField<global::CsProtobuf.CardMsg>();
    /// <summary>
    ///卡牌数组
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::CsProtobuf.CardMsg> CardItems {
      get { return cardItems_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CampInfo);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CampInfo other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (PlayerID != other.PlayerID) return false;
      if (Camp != other.Camp) return false;
      if (ItemsCount != other.ItemsCount) return false;
      if(!cardItems_.Equals(other.cardItems_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (PlayerID != 0) hash ^= PlayerID.GetHashCode();
      if (Camp != 0) hash ^= Camp.GetHashCode();
      if (ItemsCount != 0) hash ^= ItemsCount.GetHashCode();
      hash ^= cardItems_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (PlayerID != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) PlayerID);
      }
      if (Camp != 0) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Camp);
      }
      if (ItemsCount != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(ItemsCount);
      }
      cardItems_.WriteTo(output, _repeated_cardItems_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (PlayerID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) PlayerID);
      }
      if (Camp != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Camp);
      }
      if (ItemsCount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ItemsCount);
      }
      size += cardItems_.CalculateSize(_repeated_cardItems_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CampInfo other) {
      if (other == null) {
        return;
      }
      if (other.PlayerID != 0) {
        PlayerID = other.PlayerID;
      }
      if (other.Camp != 0) {
        Camp = other.Camp;
      }
      if (other.ItemsCount != 0) {
        ItemsCount = other.ItemsCount;
      }
      cardItems_.Add(other.cardItems_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            playerID_ = (global::CsProtobuf.PlayerID) input.ReadEnum();
            break;
          }
          case 16: {
            camp_ = (global::CsProtobuf.Camps) input.ReadEnum();
            break;
          }
          case 24: {
            ItemsCount = input.ReadInt32();
            break;
          }
          case 34: {
            cardItems_.AddEntriesFrom(input, _repeated_cardItems_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class InitItemPack : pb::IMessage<InitItemPack> {
    private static readonly pb::MessageParser<InitItemPack> _parser = new pb::MessageParser<InitItemPack>(() => new InitItemPack());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<InitItemPack> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::CsProtobuf.CsMsgReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitItemPack() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitItemPack(InitItemPack other) : this() {
      campInfos_ = other.campInfos_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public InitItemPack Clone() {
      return new InitItemPack(this);
    }

    /// <summary>Field number for the "campInfos" field.</summary>
    public const int CampInfosFieldNumber = 1;
    private static readonly pb::FieldCodec<global::CsProtobuf.CampInfo> _repeated_campInfos_codec
        = pb::FieldCodec.ForMessage(10, global::CsProtobuf.CampInfo.Parser);
    private readonly pbc::RepeatedField<global::CsProtobuf.CampInfo> campInfos_ = new pbc::RepeatedField<global::CsProtobuf.CampInfo>();
    /// <summary>
    ///阵营列表
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::CsProtobuf.CampInfo> CampInfos {
      get { return campInfos_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as InitItemPack);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(InitItemPack other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!campInfos_.Equals(other.campInfos_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= campInfos_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      campInfos_.WriteTo(output, _repeated_campInfos_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += campInfos_.CalculateSize(_repeated_campInfos_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(InitItemPack other) {
      if (other == null) {
        return;
      }
      campInfos_.Add(other.campInfos_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            campInfos_.AddEntriesFrom(input, _repeated_campInfos_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code