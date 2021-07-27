using System.ServiceModel.Channels;
using System.Xml;

namespace SoapCore
{
	public class CustomMessage : Message
	{
		public CustomMessage()
		{
		}

		public CustomMessage(Message message)
		{
			Message = message;
		}

		public Message Message { get; internal set; }

		public XmlNamespaceManager NamespaceManager { get; internal set; }

		public override MessageHeaders Headers
		{
			get { return Message.Headers; }
		}

		public override MessageProperties Properties
		{
			get { return Message.Properties; }
		}

		public override MessageVersion Version
		{
			get { return Message.Version; }
		}

		protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
		{
			writer.WriteStartDocument();
			var prefix = Version.Envelope.NamespacePrefix(NamespaceManager);
			writer.WriteStartElement(prefix, "Envelope", Version.Envelope.Namespace());
			writer.WriteXmlnsAttribute(prefix, Version.Envelope.Namespace());

			var xsdPrefix = Namespaces.AddNamespaceIfNotAlreadyPresentAndGetPrefix(NamespaceManager, "xsd", Namespaces.XMLNS_XSD);
			writer.WriteXmlnsAttribute(xsdPrefix, Namespaces.XMLNS_XSD);

			var xsiPrefix = Namespaces.AddNamespaceIfNotAlreadyPresentAndGetPrefix(NamespaceManager, "xsi", Namespaces.XMLNS_XSI);
			writer.WriteXmlnsAttribute(xsiPrefix, Namespaces.XMLNS_XSI);
		}

		protected override void OnWriteStartBody(XmlDictionaryWriter writer)
		{
			writer.WriteStartElement(Version.Envelope.NamespacePrefix(NamespaceManager), "Body", Version.Envelope.Namespace());
		}

		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			Message.WriteBodyContents(writer);
		}
	}
}
